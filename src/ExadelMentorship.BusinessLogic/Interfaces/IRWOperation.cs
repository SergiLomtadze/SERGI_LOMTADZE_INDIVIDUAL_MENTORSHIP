using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Features
{
    public interface IRWOperation
    {
        void WriteLine(string text);
        string ReadLine();

        void Close();
    }
}
