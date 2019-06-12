import { ItemMemberInfo } from "./ItemMemberInfo";
import { IMethodInfoDto } from "./IMethodInfoDto";

export interface IClassInfo extends ItemMemberInfo {
  methods: Array<IMethodInfoDto>;
  properties: Array<ItemMemberInfo>;
}
