# EdgeAPIserver

Simple Self-Hosted REST API server for testing purposes.
Built in C# .NET Framework (not core), using ASP Web with OWIN

Fields
Products http://localhost:2002/api/products/:
id: int (unique ID for each product - generated by the app)
name: string (product name - e.g. ‘iPad’)
price: int (price - e.g. ‘500’)

Users http://localhost:2002/api/users/:
userID: int (unique ID for each user - generated by the app)
userName: string

Authorization
There is no authorization required for products
For Users, any requests require basic authorization (user:password), credentials are:
edge:edgewords
Header would be:
Authorization: Basic ZWRnZTplZGdld29yZHM=
