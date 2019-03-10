using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace MeeksTemplateBuilder
{
    public class MeeksTemplater
    {
        IMeeksTemplateProvider TemplateProvider = null;

        public string render(string html, dynamic obj)
        {
            html = AddExternalTemplates(html);
            var PropertyList = new Dictionary<string, object>();
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
            {
                object value = prop.GetValue(obj);
                PropertyList.Add(prop.Name, value);
            }
            Regex regex = new Regex(@"\[.*?\]");

            Match match = regex.Match(html);
            while (match.Success)
            {
                string TextValue = "";
                object Value = null;
                string property = match.Value.Replace("[", "").Replace("]", "");
                if (PropertyList.TryGetValue(property, out Value))
                {
                    TextValue = Value.ToString();
                }
                html = html.Replace(match.Value, TextValue);
                match = regex.Match(html);
            }
            return html;
        }

        private string AddExternalTemplates(string html)
        {
            if (TemplateProvider == null)
            {
                return html;
            }
            Regex regex = new Regex(@"\[\[.*?\]\]");

            Match match = regex.Match(html);
            while (match.Success)
            {
                string templateName = match.Value.Replace("[[", "").Replace("]]", "");
                string template = TemplateProvider.GetTemplate(templateName);
                html = html.Replace(match.Value, template);
                match = regex.Match(html);
            }
            return html;
        }

        public void AddTemplateProvider(IMeeksTemplateProvider provider)
        {
            TemplateProvider = provider;
        }
    }
}
