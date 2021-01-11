using System;
using System.Collections.Generic;
using System.Text;
using CodeGenHero.Inflector;
using Microsoft.EntityFrameworkCore.Design;

namespace CGHClientServer1.DB.Pluralizer
{
    public class InflectorWrapper : IPluralizer
    {
        private readonly ICodeGenHeroInflector _inflector = new CodeGenHeroInflector();

        public string Pluralize(string identifier)
        {
            var retVal = _inflector.Pluralize(identifier);
            return retVal;
        }

        public string Singularize(string identifier)
        {
            var retVal = _inflector.Singularize(identifier);
            return retVal;
        }
    }
}