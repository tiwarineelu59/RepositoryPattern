Considering this as a small application best way is to make it in a repository pattern without making it complex and also made sure to use best practices and oops principles  

Abstraction of Data Access Logic: The Repository pattern helps abstract data access logic from the rest of the application. This separation allows for easier maintenance and testing, as changes to the data access layer can be isolated.
Dependency Injection: We would leverage dependency injection to inject the appropriate repository implementations
Centralized Data Access Logic: Repositories centralize data access logic, providing a single point of entry for accessing and manipulating data. This can simplify the codebase and reduce duplication of data access code across multiple components.


**Authentication and Authorization**

Token based authention can be implemented by  using required keys like IssuerSigningKey, Audience , Issuer Key from azure

**Database**

I have used the Stored procedure which helps prevent SQL Injection parameterizing queries and enabling input validation. 

Better Performance: The procedure calls are quick and efficient as stored procedures are compiled once and stored in executable form. Hence the response is quick. The executable code is automatically cached, hence lowering the memory requirements.

Higher Productivity: The stored Procedure helps to encapsulate the SQL logic and business logic which provides reusability and modularity.


**Testing**

Included unit test cases

**Logging**

I have just used console logging of exception however we can also implement other logging  and monitoring mechnism using Azure or any other mechnism



