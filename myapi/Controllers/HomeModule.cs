using myapi.Services;
using Nancy;
using FluentValidation;
using Nancy.Validation;

namespace myapi.Controllers
{
    public class HomeModule : NancyModule
    {
        public HomeModule(IIpsumService ipsumService)
        {
            Get("/", args => "Hello from Nancy running on CoreCLR");
            Get("/test/{name}", args => GetPersonName(new Person {Name = args.name}));
            Get("/ipsum", async args => await ipsumService.GenerateAsync());
        }

        private Person GetPersonName(Person person)
        {
            var valid = this.Validate(person);
            if(!valid.IsValid){
                return new Person {Name = "Don't know Hello :)"};
            }

            return person;
        }
    }
 
    public class Person
    {
        public string Name { get; set; }
    }

    public class NameRequestValidator : AbstractValidator<Person>
    {
        public NameRequestValidator()
        {
            RuleFor(request => request.Name).NotEqual("hello");
        }
    }
}