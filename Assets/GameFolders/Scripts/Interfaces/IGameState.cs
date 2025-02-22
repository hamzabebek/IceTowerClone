using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameFolders.Interfaces
{
    internal interface IGameState
    {
        bool IsGameOver { get; set; }
    }
}
