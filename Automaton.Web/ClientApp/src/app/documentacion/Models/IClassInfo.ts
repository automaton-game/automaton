import { ItemMemberInfo } from "./ItemMemberInfo";
import { IMethodInfoDto } from "./IMethodInfoDto";

export interface IClassInfo extends ItemMemberInfo {
  items: Array<IMethodInfoDto>;
}
