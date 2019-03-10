using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    class Application
    {
        public void run()
        {
            TemplateGenerator templateGenerator = new TemplateGenerator();
            string result = templateGenerator.GeneratePresentationText();
            Console.WriteLine(result);
        }
    }
}
