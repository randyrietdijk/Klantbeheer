# Over
Klantbeheer is een minimalistische applicatie voor het beheren van klanten. De solution heeft 3 web projecten:
- CustomerManagement.IDP, de identity provider (OAuth2.0);
- CustomerManagement.API, de API met een in-memory EF database;
- CustomerManagement.UI, de MVC applicatie met de formulieren; 

Naast de web projecten bevat de solution ook een aantal class libraries:
- Common.Data voor command-query seperation;
- Common.ApiClient voor API calls;
- CustomerManagement.ApiClient als implementatie van CustomerManagement.API;
- CustomerManagement.ApiClient.DependencyInjection als DI extensie bovenop CustomerManagement.ApiClient;

# Starten
De IDP, API en UI dienen alle 3 gestart te worden om de totale applicatie draaiend te hebben.

# Impressie
Afbeeldingen van de IDP en UI:

![Inlogscherm van de IDP](https://user-images.githubusercontent.com/55363461/194921336-adcef507-fea9-4373-bc5a-14917428d6a8.png)
![Beheerscherm van de UI](https://user-images.githubusercontent.com/55363461/194921371-a3c771aa-fb74-450a-97fa-12bd7ce5a69e.png)
