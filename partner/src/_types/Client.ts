export type AuthProfileApiProps = {
  id?: string;
  fullName?: string;
  sysRoleId?: string;
  masterSaleGroupId?: string;
};

export type AuthProfileProps = {
  id?: string;
  fullName?: string;
  role?: RoleCode;
  permission?: string[];
  masterSaleGroupId?: string;
};

export type SysPermissionProps = {
  page?: string;
};

export type ClientDetailFromProps = {
  clientData?: TransClientProps;
};

export type ClientInfoTabContentFromProps = {
  clientData?: TransClientProps;
  comment?: string;
  comments?: TransClientCommentProps[];
  onAction?: "" | "comment" | "update";
};

export type TransClientCommentProps = {
  description?: string;
  updatedDate?: Date;
  updatedBy?: string;
};
