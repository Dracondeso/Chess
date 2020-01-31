using ChessOnline.Models;
using ChessOnline.Models.Board;
using ChessOnline.Models.Board.Pieces.Abstraction;
using ChessOnline.Models.Enum;
using ChessOnline.Models.Primitives;
using Newtonsoft.Json;
using Engine.Networking;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
namespace Engine
{
    public class Core
    {
        private DataModel ToClient = new DataModel();
        private List<User> WaitingList = new List<User>();
        public DataModel Elaborate(DataModel dataClient)
        {
            ToClient = dataClient;
            if (WaitingList.Count == 1)
            {
                if (ToClient.User.RoomKey == WaitingList[0].RoomKey)
                {
                    ToClient.serverOperation = ServerOperationType.SendToClient;
                    return ToClient;
                }
            }


            if (ToClient.User.RoomKey != null && ToClient.User.StartPosition == null)
            {
                ToClient.serverOperation = ServerOperationType.AssignColorOperation;
            }

            while (ToClient.serverOperation != ServerOperationType.SendToClient)
            {
                switch (ToClient.serverOperation)
                {
                    case ServerOperationType.LogInOperation:
                        WaitingManage(dataClient);
                        break;
                    case ServerOperationType.MoveOperation:

                        break;
                    case ServerOperationType.AssignColorOperation:
                        AssignColor();

                        break;
                    case ServerOperationType.CreateRoomOperation:
                        CreateRoom(WaitingList);
                        break;
                    case ServerOperationType.SendToClient:
                        break;
                    default:
                        break;

                }
            }
            return ToClient;
        }
        public DataModel WaitingManage(DataModel dataClient)
        {
            if (WaitingList.Count == 0)
            {
                WaitingList.Add(dataClient.User);
                WaitingList[0].RoomKey = Guid.NewGuid().ToString();
                ToClient = dataClient;
                ToClient.serverOperation = ServerOperationType.SendToClient;
            }
            if (WaitingList.Count == 1 && WaitingList[0].UserName != dataClient.User.UserName)
            {
                WaitingList.Add(dataClient.User);
                WaitingList[1].RoomKey = WaitingList[0].RoomKey;
                ToClient = dataClient;
                ToClient.serverOperation = ServerOperationType.CreateRoomOperation;
            }

            return ToClient;
        }
        public DataModel CreateRoom(List<User> users)
        {
            Room.Instance(users[0].RoomKey).Users.AddRange(users);
            WaitingList.Clear();
            ToClient.serverOperation = ServerOperationType.AssignColorOperation;

            return ToClient;
        }
        private Vector Cardinal(Direction direction, Vector position, double increment)
        {
            Vector check = new Vector();
            switch (direction)
            {
                default:
                    throw new NotImplementedException("Unrecognized value.");
                case Direction.north:
                    check.Update(position.X, position.Y + increment);
                    return check;
                case Direction.south:
                    check.Update(position.X, position.Y - increment);
                    return check;
                case Direction.east:
                    check.Update(position.X + increment, position.Y);
                    return check;
                case Direction.west:
                    check.Update(position.X - increment, position.Y);
                    return check;
                case Direction.northEast:
                    check.Update(position.X + increment, position.Y + increment);
                    return check;
                case Direction.northWest:
                    check.Update(position.X - increment, position.Y + increment);
                    return check;
                case Direction.southEast:
                    check.Update(position.X + increment, position.Y - increment);
                    return check;
                case Direction.southWest:
                    check.Update(position.X - increment, position.Y - increment);
                    return check;
            }
        }
        public Dictionary<string, Piece> UpdatedBoard(DataModel dataClient)
        {


            Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard.Remove(dataClient.User.StartPosition);
            if (Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard.ContainsKey(dataClient.User.EndPosition))
            {
                Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard.Remove(dataClient.User.EndPosition);
            }
            Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard.Add(dataClient.User.EndPosition, Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard[dataClient.User.StartPosition]);

            return Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard;
        }
        public List<Vector> Behavior(DataModel dataClient)
        {


            for (double i = 0; i == 8; i++)
            {
                Direction direction1 = (Direction)i;
                if (Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard[dataClient.User.StartPosition].DirectionSteps[(int)i] != 0)
                {
                    for (double j = 1; j <= Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard[dataClient.User.StartPosition].DirectionSteps[(int)i]; j++)
                    {
                        if (Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard.ContainsKey(Cardinal(direction1, Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard[dataClient.User.StartPosition].StartPosition, j).ToString()))
                        {
                            Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard.TryGetValue(Cardinal(direction1, Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard[dataClient.User.StartPosition].StartPosition, j).ToString(), out Piece piece1);
                            Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard.TryGetValue(Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard[dataClient.User.StartPosition].StartPosition.ToString(), out Piece piece2);
                            if (piece1.Side == piece2.Side || piece2.Name.Equals(PieceType.King))
                            {
                                break;
                            }
                            else
                            {
                                Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard[dataClient.User.StartPosition].Checks.Add(Cardinal(direction1, Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard[dataClient.User.StartPosition].StartPosition, i));
                                break;
                            }
                        }
                        Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard[dataClient.User.StartPosition].Checks.Add(Cardinal(direction1, Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard[dataClient.User.StartPosition].StartPosition, i));
                    }
                }
            }
            return Room.RoomsMultitone[ToClient.User.RoomKey].Board.ChessBoard[dataClient.User.StartPosition].Checks;
        }
        public DataModel AssignColor()
        {
            Room.RoomsMultitone[ToClient.User.RoomKey].Users[0].Side = Side.White;
            Room.RoomsMultitone[ToClient.User.RoomKey].Users[0].YourTurn = true;
            Room.RoomsMultitone[ToClient.User.RoomKey].Users[1].Side = Side.Black;
            if (Room.RoomsMultitone[ToClient.User.RoomKey].Users[0].UserName == ToClient.User.UserName)
            {
                ToClient.User = Room.RoomsMultitone[ToClient.User.RoomKey].Users[0];
            }
            else
            {
                ToClient.User = Room.RoomsMultitone[ToClient.User.RoomKey].Users[1];
            }
            ToClient.serverOperation = ServerOperationType.SendToClient;
            return ToClient;
        }
    }
}

//    public static Piece FindPiece(Vector startPosition, Dictionary<string, Piece> chessBoard)
//    {
//        chessBoard.TryGetValue(startPosition.ToString(), out Piece piece);
//        return piece;
//    }
//    public void cose()
//    {
//        if (room == null)
//        {
//            AssignRoom(user, state);
//            return user;
//        }
//        if ((room != null) && (room.Users.Count == 1))
//        {
//            return user;
//        }
//        else
//        {
//            foreach (Vector check in FindPiece(user.StartPosition, room.Board.ChessBoard).Move(user))
//            {
//                if (check.Equals(user.EndPosition))
//                {
//                    foreach (User user1 in room.Users)
//                    {
//                        if (user.YourTurn == true)
//                        {
//                            if (user.UserName != user1.UserName)
//                            {
//                                Console.WriteLine("Mossa Consentita");
//                                SendMove(user, user1, room);
//                                return user;
//                            }
//                        }
//                        else
//                        {
//                            Console.WriteLine("Non e` il tuo turno");
//                        }
//                    }
//                }
//            }
//            return user;
//        }

//    }
//}
