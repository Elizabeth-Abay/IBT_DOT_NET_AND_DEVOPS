// in middleware
// app.Use( middleware func)
// it receives 2 parameters context - the request and next - go to the next
// it will intercept both the req and the res
// and it will be like a stack and all the other middlewares like the controller..
// will be stacked up in the stack