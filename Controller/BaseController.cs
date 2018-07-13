using bazadanych.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazadanych.Controller
{

    //kontroler bazowy
    public class BaseController : IDisposable
    {
        public bool Disposed { get; private set; }
        protected WarsztatContext db = new WarsztatContext();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

        public void zglosBlad(string messageFormat = "", params object[] args)
        {
            if (messageFormat == "")
                messageFormat = "Operacja nie powiodła się.";
            throw new Exception(String.Format(messageFormat, args));
        }
    }
}
