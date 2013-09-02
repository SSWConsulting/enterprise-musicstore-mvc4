enterprise-music-store
======================

The Enterprise Music Store takes the MvcMusicStore sample from ASP.Net and adds Dependency Injection, Unit Tests and a more maintainable architecture.

The original music store application can be found here
http://mvcmusicstore.codeplex.com/

Primary changes to the Music Store Application
- Implement Dependency Injection based architecture (<a href="http://jeffreypalermo.com/blog/the-onion-architecture-part-1/" target="_new">The Onion Architecture</a>)
- Implement <a href="http://www.ninject.org/" target="_new">Ninject</a> for Dependency Injection 
- Implement <a href="http://nsubstitute.github.io/" target="_new">NSubstitute</a> for Mocking
- ToDo: Implement <a href="http://sqldeploy.com/" target="_new">SSW.SQLDeployMVC</a> for easy database script deployment


