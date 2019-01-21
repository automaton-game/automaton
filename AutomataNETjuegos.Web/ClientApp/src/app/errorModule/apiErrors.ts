import { ApiError } from "./apiError";

export class ApiErrors extends Error {
  constructor(public ApiError: ApiError[]) {
    super();
  }
}
