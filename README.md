# brewjournal
An asp.net mvc 5 web application to record the home brewing process.  From processes taken on brew day, to observations throughout fermentation, to recipes and reviews of the final product.

# Technologies
This is a bit of a playground in using different techniques to keep code clean, simple and a high amount of test coverage.

The following ideas have been implemented:

- Feature folders - well covered all about, using custom view resolution in FeatureViewLocationRazorViewEngine
- Action per controller (GET/POST) - adapted from jak charlton's [concept](http://devlicio.us/blogs/casey/archive/2011/07/11/single-action-per-controller-in-asp-net-mvc.aspx) with implementation in ActionPerControllerFactory
- Subcutaneous testing - taken from rob moore's [post](https://robdmoore.id.au/blog/2015/01/26/review-of-jimmy-bogard-holistic-testing/) on the subject.

# Roadmap
On top of using this to record home brews, there are some concepts that would be good to prove:

- Structured logging
- Mobile application integration
- Convention tests
