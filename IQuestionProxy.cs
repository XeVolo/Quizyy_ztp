using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf
{
    public interface IQuestionProxy : ISaveProxy
    {
        string SaveQuestion();
        string SaveCorrectAnswer();
        string SaveUncorrectAnswer1();
        string SaveUncorrectAnswer2();
        string SaveUncorrectAnswer3();
        string SaveLevel();
    }
}
