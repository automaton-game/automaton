import { IClassInfo } from "./IClassInfo";

export interface INameSpaceInfo {
  name: string;
  classes: Array<IClassInfo>;
  nameSpaces: Array<INameSpaceInfo>;
}
