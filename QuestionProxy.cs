using Quizyy_wpf.Model;

namespace Quizyy_wpf
{
    public class QuestionProxy : IQuestionProxy
    {
        private readonly string _question;
        private readonly string _answer;
        private readonly string _incorrectans1;
        private readonly string _incorrectans2;
        private readonly string _incorrectans3;
        private readonly string _difficultylvl;
        public QuestionProxy(string question, string answer, string incorrectans1, string incorrectans2, string incorrectans3, string difficultylvl)
        {
            _question = question;
            _answer = answer;
            _incorrectans1 = incorrectans1;
            _incorrectans2 = incorrectans2;
            _incorrectans3 = incorrectans3;
            _difficultylvl = difficultylvl;
        }

        public string TakeQuestion()
        {
            return _question;
        }
        public string TakeCorrectAnswer()
        {
            return _answer;
        }
        public string TakeIncorrectAnswer1()
        {
            return _incorrectans1;
        }
        public string TakeIncorrectAnswer2()
        {
            return _incorrectans2;
        }
        public string TakeIncorrectAnswer3()
        {
            return _incorrectans3;
        }
        public string TakeLevel()
        {
            return _difficultylvl;
        }
        public void Save()
        {
            using (var context = new MyBaseContext())
            {
                var isExist = context.Writes.FirstOrDefault(f => f.question == _question);
                var lastId = context.Writes.OrderByDescending(f => f.id).Select(f => f.id).FirstOrDefault();

                if (isExist == null)
                {
                    var newQuestion = new WriteModel
                    {
                        id = lastId + 1,
                        question = _question,
                        answer = _answer,
                        incorrectans1 = _incorrectans1,
                        incorrectans2 = _incorrectans2,
                        incorrectans3 = _incorrectans3,
                        difficultylvl = _difficultylvl
                    };
                    context.Writes.Add(newQuestion);
                    context.SaveChanges();
                }

                context.Dispose();
            }
        }

    }
}
