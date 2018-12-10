using System;
using System.Linq;
using StructureMap;
using MediatR;

namespace CodingChallenge.UI.Api
{
    public class ApiRegistry : Registry
    {
        public ApiRegistry()
        {
            Scan(s =>
            {
                foreach (var currentAssembly in AppDomain.CurrentDomain.GetAssemblies()
                                                         .Where(x => x.FullName.Contains("CodingChallenge")))
                    s.Assembly(currentAssembly);
                
                s.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
                s.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));

                s.RegisterConcreteTypesAgainstTheFirstInterface();
            });
        }
    }
}