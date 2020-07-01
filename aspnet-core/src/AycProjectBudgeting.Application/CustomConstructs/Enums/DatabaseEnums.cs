﻿namespace AYCProjectBudgeting.CustomConstructs.DatabaseEnums
{
    public enum EFirmRoleInConsortium
    {
        Leader = 1,
        Member = 2
    }

    public enum EStatus
    {
        Forecast = 1,
        Open = 2,
        Closed = 3,
        Shortlisted = 4,
        Awarded = 5,
        Cancelled = 6,
    }

    public enum ETenderType
    {
        Service = 1,
        Supply = 2,
        Work = 3,
        Grant = 4
    }

    public enum EFirmRole
    {
        Leader = 1,
        Member = 2
    }

    public enum EGeneralSettings
    {
        MaxFirmInConsortium = 8
    }
}