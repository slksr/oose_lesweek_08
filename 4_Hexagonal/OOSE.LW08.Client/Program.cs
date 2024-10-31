using System.Threading.Tasks;

namespace OOSE.LW08.Client
{
    internal class Program
    {
        /// <summary>
        /// De verantwoordelijkjeid van Main is het starten van de applicatie.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            App app = new App();
            await app.Initialize();
            await app.Run();
        }
    }
}