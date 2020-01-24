using ChessOnline.Models.Board;
using ChessOnline.Models.Board.Pieces.Abstraction;
using ChessOnline.Models.Enum;
using ChessOnline.Models.Primitives;
using Newtonsoft.Json;
using Server.Networking;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
namespace Server
{
    public static class Core
    {
        public static Socket Listener;
        private static List<Room> Rooms = new List<Room>();
        public static Dictionary<User, StateObject> StateObjects = new Dictionary<User, StateObject>();
        public static User Elaborate(User user, StateObject state)
        {
                Room room = FindLoggedUserRoom(user);
            if (room == null)
            {
               AssignRoom(user, state);
                return user;
            }
            if ((room!=null) && (room.Users.Count==1))
            {
                return user;
            }
            else
            {
                foreach (Vector check in FindPiece(user.StartPosition, room.Board.ChessBoard).Move(user))
                {
                    if (check.Equals(user.EndPosition))
                    {
                        foreach (User user1 in room.Users)
                        {
                            if (user.YourTurn == true)
                            {
                                if (user.UserName != user1.UserName)
                                {
                                    Console.WriteLine("Mossa Consentita");
                                    SendMove(user, user1, room);
                                    return user;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Non e` il tuo turno");
                            }
                        }
                    }
                }
                return user;
            }
        }
        public static User AssignRoom(User user, StateObject stateObject)
        {
                stateObject.SetState(user);
            if (Rooms.Count == 0)
            {
                Room room1 = new Room("room0");
                Rooms.Add(room1);
                user.SetRoomKey(room1);
                room1.Users.Add(user);
                return user;
            }
            foreach (Room room in Rooms)
            {
                if (room.Users.Count == 1)
                {
                    user.SetRoomKey(room);
                    room.Users.Add(user);
                    room.Users[0].YourTurn = true;
                    room.Users[1].YourTurn = false;
                    room.Users[0].Side = Side.White;
                    room.Users[1].Side = Side.Black;
                    string jsonToWhite = JsonConvert.SerializeObject(room.Users[0]);
                    string jsonToBlack = JsonConvert.SerializeObject(room.Users[1]);
                    StateObjects.TryGetValue(room.Users[0], out StateObject white);
                    StateObjects.TryGetValue(room.Users[1], out StateObject black);
                    byte[] toWhite = (Encoding.ASCII.GetBytes(jsonToWhite));
                    byte[] toBlack = (Encoding.ASCII.GetBytes(jsonToBlack));
                    white.workSocket.Send(toWhite);
                    black.workSocket.Send(toBlack);
                    //AsynchronousSocketListener.Send(white.workSocket, jsonToWhite);
                    //AsynchronousSocketListener.Send(black.workSocket, jsonToBlack);


                    //AsynchronousSocketListener.Send(black.workSocket, jsonBlack);
                    return user;
                }
            }
            string roomName = $"room{Rooms.Count}";
            Room room2 = new Room(roomName);
            Rooms.Add(room2);
            user.SetRoomKey(room2);
            room2.Users.Add(user);
            return user;
        }
        public static void SendMove(User userTurn, User otherUser, Room room)
        {
            UpdateBoard(userTurn, room);
            otherUser.ChessBoard = userTurn.ChessBoard;
            StateObjects.TryGetValue(userTurn, out StateObject objectTurn);
            StateObjects.TryGetValue(otherUser, out StateObject otherObject);
            userTurn.YourTurn = false;
            otherUser.YourTurn = true;

            string jsonTurn = JsonConvert.SerializeObject(userTurn);
            string otherJson = JsonConvert.SerializeObject(otherUser);
            //AsynchronousSocketListener.Send(objectTurn.workSocket, jsonTurn);
            //AsynchronousSocketListener.Send(otherObject.workSocket, otherJson);

        }
        public static Room FindLoggedUserRoom(User user)
        {
            foreach (Room room in Rooms)
            {
                foreach (User userInList in room.Users)
                {
                    if (userInList.UserName == user.UserName)
                    {
                        return room;
                    }
                }
            }
            return null;
        }
        private static Vector Cardinal(Direction direction, Vector position, double increment)
        {
            Vector check = new Vector();
            switch (direction)
            {
                default:
                    throw new NotImplementedException("Unrecognized value.");
                case Direction.north:
                    check.Update(position.X, (position.Y + increment));
                    return check;
                case Direction.south:
                    check.Update(position.X, (position.Y - increment));
                    return check;
                case Direction.east:
                    check.Update((position.X + increment), position.Y);
                    return check;
                case Direction.west:
                    check.Update((position.X - increment), position.Y);
                    return check;
                case Direction.northEast:
                    check.Update((position.X + increment), (position.Y + increment));
                    return check;
                case Direction.northWest:
                    check.Update((position.X - increment), (position.Y + increment));
                    return check;
                case Direction.southEast:
                    check.Update((position.X + increment), (position.Y - increment));
                    return check;
                case Direction.southWest:
                    check.Update((position.X - increment), (position.Y - increment));
                    return check;
            }
        }
        public static StateObject GetStateObject(User user)
        {
            if (StateObjects.ContainsKey(user))
            {
                StateObjects.TryGetValue(user, out StateObject GettedState);
                return GettedState;
            }
            else
                return null;

        }
        public static Dictionary<string, Piece> UpdateBoard(User user, Room room)
        {
            user.ChessBoard.TryGetValue(user.StartPosition.ToString(), out Piece piece);
            user.ChessBoard.Remove(user.StartPosition.ToString());
            user.ChessBoard.Remove(user.EndPosition.ToString());
            user.ChessBoard.Add(user.EndPosition.ToString(), piece);
            room.Board.ChessBoard = user.ChessBoard;
            return user.ChessBoard;
        }
        public static List<Vector> Behavior(Vector startPosition, User user)
        {
            Piece piece = FindPiece(startPosition, user.ChessBoard);
            for (double i = 0; i == 8; i++)
            {
                Direction direction1 = (Direction)i;
                if (piece.DirectionSteps[(int)i] != 0)
                {
                    for (double j = 1; j <= piece.DirectionSteps[(int)i]; j++)
                    {
                        if (user.ChessBoard.ContainsKey(Cardinal(direction1, startPosition, j).ToString()))
                        {
                            user.ChessBoard.TryGetValue(Cardinal(direction1, startPosition, j).ToString(), out Piece piece1);
                            user.ChessBoard.TryGetValue(startPosition.ToString(), out Piece piece2);
                            if ((piece1.Side == piece2.Side) || (piece2.Name.Equals(PieceType.King)))
                            {
                                break;
                            }
                            else
                            {
                                piece.Checks.Add(Cardinal(direction1, startPosition, i));
                                break;
                            }
                        }
                        piece.Checks.Add(Cardinal(direction1, startPosition, i));
                    }
                }
            }
            return piece.Checks;
        }
        public static Piece FindPiece(Vector startPosition, Dictionary<string, Piece> chessBoard)
        {
            chessBoard.TryGetValue(startPosition.ToString(), out Piece piece);
            return piece;
        }
    }

}
