using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Features
{
    public interface IConsoleOperation
    {
        void WriteLine(string text);
        string ReadLine();
    }
}
