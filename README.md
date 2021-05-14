# MVC Cascading Drop Downs

The purpose of this repository is to demonstrate two different ways of producting a cascading drop in ASP.NET Core MVC. This will work using .NET Framework as well.

The first way has pure client side javascript filtering where all of the items for both the parent and child are rendered in the DOM, and only after changes are made in the parent drop down will JavvaScript manually rebuild the child Select List Options.

The second way uses AJAX calls where AJAX calls are made every time the parent Select List is changed and a JavaScript creates the appropriate drop down elements based on the AJAX response.