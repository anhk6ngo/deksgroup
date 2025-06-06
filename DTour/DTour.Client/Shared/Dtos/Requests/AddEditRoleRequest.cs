﻿namespace DTour.Client.Shared.Dtos.Requests;

public class AddEditRoleRequest
{
    public RoleUserDto? Data { get; set; }
    public ActionCommandType Action { get; set; } = ActionCommandType.Add;
    public bool IsSetRight { get; set; } = false;
}