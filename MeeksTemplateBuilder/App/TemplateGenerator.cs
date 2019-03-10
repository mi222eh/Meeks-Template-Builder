using MeeksTemplateBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    class TemplateGenerator  : IMeeksTemplateProvider
    {
        public string GetTemplate(string name)
        {
            return Templates.GetTemplate(name);
        }

        public string GeneratePresentationText()
        {
            MeeksTemplater templater = new MeeksTemplater();
            templater.AddTemplateProvider(this);
            return templater.render(Templates.Presentation, new
            {
                TOOL_NAME = "Meeks template builder",
                COMPANY_NAME = "Marvelous Cookie Factory",
                START_DATE = "1970",
                ACTIVE_TEXT = "Not active",
                PRODUCT = "Cookies",
                EXISTS_TEXT = "No"
            });
        }
    }
}
