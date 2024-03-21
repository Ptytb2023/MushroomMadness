
using System.Collections;

namespace MushroomMadness.Controllers
{
    public interface IMove
    {
        public IEnumerator Move();
        public IEnumerator Jump();
    }
}
