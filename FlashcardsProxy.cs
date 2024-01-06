using Microsoft.EntityFrameworkCore;
using Quizyy_wpf.Model;

namespace Quizyy_wpf
{
    public class FlashCardsProxy : IFlashCardsProxy
    {
        private readonly string _concept;
        private readonly string _definition;
        private readonly string _difficultylvl;
        public FlashCardsProxy(string concept, string definition, string level)
        {
            _concept = concept;
            _definition = definition;
            _difficultylvl = level;
        }

        public string TakeConcept()
        {
            return _concept;
        }

        public string TakeDefinition()
        {
            return _definition;
        }
        public string TakeLevel()
        {
            return _difficultylvl;
        }

        public void Save()
        {
            using (var context = new MyBaseContext())
            {
                var isExist = context.FlashCards.FirstOrDefault(f => f.concept == _concept && f.definition == _definition);      
                var lastId = context.FlashCards.OrderByDescending(f => f.id).Select(f => f.id).FirstOrDefault();       

                if (isExist == null)
                {
                    var newFlashcard = new FlashCardsModel
                    {
                        id = lastId + 1,
                        concept = _concept,
                        definition = _definition,
                        difficultylvl = _difficultylvl
                    };
                    context.FlashCards.Add(newFlashcard);
                    context.SaveChanges();
                }
                            
                context.Dispose();
            }
        }
    }
}
