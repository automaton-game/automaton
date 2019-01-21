export class ApiError extends Error {
  constructor(message?: string, name?: string, stack?: string) {
    super(message);
    this.stack = stack;
    this.name = name;
  }
}
