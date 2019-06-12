import { ItemMemberInfo } from "./ItemMemberInfo";

export interface IMethodInfoDto extends ItemMemberInfo {
  params: Array<ItemMemberInfo>;
}
