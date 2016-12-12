# SSW Enterprise Music Store MVC 4

## Warning: This project is no longer maintained


The SSW Enterprise Music Store MVC 4 takes the MvcMusicStore sample from ASP.Net and adds Dependency Injection, Unit Tests and a more maintainable architecture.

The original music store application can be found here
<a href="http://mvcmusicstore.codeplex.com/" target="_new">http://mvcmusicstore.codeplex.com/</a>

### Primary changes to the Music Store Application
- Implement <a href="http://rules.ssw.com.au/SoftwareDevelopment/RulesToBetterMVC/Pages/Use-a-Dependency-Injection-Centric-Architecture.aspx" target="_new">Dependency Injection based architecture</a> like the <a href="http://jeffreypalermo.com/blog/the-onion-architecture-part-1/" target="_new">Onion Architecture</a>
- Implement <a href="http://www.ninject.org/" target="_new">Ninject</a> for Dependency Injection 
- Implement <a href="http://nsubstitute.github.io/" target="_new">NSubstitute</a> for Mocking
- ToDo: Implement <a href="http://sqldeploy.com/" target="_new">SSW.SQLDeployMVC</a> for easy database script deployment (Currently uses LocalDb & Code First)

