using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf
{
    public interface IQuestionProxy 
    {
        string TakeQuestion();
        string TakeCorrectAnswer();
        string TakeIncorrectAnswer1();
        string TakeIncorrectAnswer2();
        string TakeIncorrectAnswer3();
        string TakeLevel();
        void Save();
    }
}
