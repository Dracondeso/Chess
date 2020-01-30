using System;
using System.Collections.Generic;
using System.Text;

namespace ChessOnline.Models.Enum
{
  public enum ServerOperationType
    {
        LogInOperation,
        MoveOperation,
        AssignColorOperation,
        CreateRoomOperation,
        SendToClient,
        
    }
}
