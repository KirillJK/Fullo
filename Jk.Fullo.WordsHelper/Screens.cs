using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jk.Fullo.WordsHelper
{
    public class Screens: IScreens
    {
        private List<IScreen> _screens = new List<IScreen>();
        public async Task<bool> Print(Word fullWord, string language)
        {
            bool result = true;
            foreach (var screen in _screens)
            {
                result = result && await screen.Print(fullWord, language);
            }
            return result;
        }

        public void RegisterScreen(IScreen screen)
        {
            _screens.Add(screen);
        }
    }
}