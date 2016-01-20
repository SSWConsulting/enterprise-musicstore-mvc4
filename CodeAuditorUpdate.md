Using the SSW Code Auditor this project is down to zero errors. Fixes include:
	• Added braces {} around if statements (AccountController.cs)
	• Suffixed test classes with "Tests" 
	• Renamed e-mail to email (AccountController.cs)
	• Renamed "set up" to "configure" (ShopppingCartController.cs)
	• Renamed 'favourite' to 'favorite' (Class1.cs)
	• Added <remove path*/> (Web.config)
	• Incorrectly found 2 unspecific Exception errors in commented out code, rewrote rule as Roslyn to resolve
	• Specified an unspecific Exceptions as 'Code Auditor Ignore' as their functionality was correct (AccountController.cs)
	• Made exceptions more specific (AccountController.cs, InitializeSimpleMembershipAttribute.cs)
