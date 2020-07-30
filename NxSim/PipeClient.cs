using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game1
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
                string id = sr.ReadLine();
                EquipTypes et = EquipTypesExtension.GetEquipTypeFromId(id);
                id = FormatId(id);
                
                XmlLoader.LoadXml(game.GraphicsDevice, et, id);
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
