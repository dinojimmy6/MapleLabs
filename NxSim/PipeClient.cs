using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NxSim
{
    class PipeClient
    {
        public StreamReader sr;
        public EventWaitHandle ev;
        public NxSim game;

        public PipeClient(string handle, NxSim game)
        {
            PipeStream pipeClient = new AnonymousPipeClientStream(PipeDirection.In, handle);
            sr = new StreamReader(pipeClient);
            ev = EventWaitHandle.OpenExisting("NxPipe");
            this.game = game;
        }

        public void Listen(object gfxd)
        {
            while(true)
            {
                ev.WaitOne();
                string line = sr.ReadLine();
                string id = line.Split('-')[0];
                string weaponBase = line.Split('-')[1];
                EquipTypes et = EquipTypesExtension.GetEquipTypeFromId(id);
                id = FormatId(id);
                if(Int32.Parse(id) / 10000 <= 120)
                {
                    XmlLoader.LoadXml(game.GraphicsDevice, et, id);
                }
                else
                {
                    XmlLoader.LoadWeaponXml(game.GraphicsDevice, et, id, weaponBase);
                }
                game.UpdateCharacter();
            }
        }

        private string FormatId(string id)
        {
            while(id.Length < 8)
            {
                id = "0" + id;
            }
            return id;
        }
    }
}
