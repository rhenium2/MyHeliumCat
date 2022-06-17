using HeliumInsighter.Responses;

namespace HeliumInsighter;

public static class Constants
{
    public class TransactionType
    {
        public static string Rewards = "rewards_v2";
        public static string PocReceipts = "poc_receipts_v2";
        public static string StateChannelClose = "state_channel_close_v1";
    }

    // https://github.com/helium/explorer-api/blob/6d4236c753026ceab98326d1fd503a1ecab4c9be/src/helpers/makers.js#L22
    public static Maker[] Makers = new[]
    {
        new Maker { Name = "Helium Inc", Address = "13daGGWvDQyTyHFDCPz8zDSVTWgPNNfJ4oh31Teec4TRWfjMx53" },
        new Maker { Name = "Bobcat", Address = "14sKWeeYWQWrBSnLGq79uRQqZyw3Ldi7oBdxbF6a54QboTNBXDL" },
        new Maker { Name = "SenseCAP", Address = "14NBXJE5kAAZTMigY4dcjXSMG4CSqjYwvteQWwQsYhsu2TKN6AF" },
        new Maker { Name = "Linxdot", Address = "14eUfY1GsjK4WH6uZYoeagnFtigBKdvPruAXLmc5UsUMEDj3yib" },
        new Maker { Name = "PantherX", Address = "13v9iGhjvQUtVaZXcFFRCEbL1nPR4R8QJowBgMUcaGM2v1aV6mn" },
        new Maker { Name = "Milesight", Address = "13H6RpJojJjkqPRfzdrFBDbpYw4b3A6HyMxgGFRgXf31Nuwq4xJ" },
        new Maker { Name = "CalChip Connect", Address = "13ENbEQPAvytjLnqavnbSAzurhGoCSNkGECMx7eHHDAfEaDirdY" },
        new Maker { Name = "Bobcat 5G", Address = "14gqqPV2HEs4PCNNUacKVG7XeAhCUkN553NcBVw4xfwSFcCjhXv" },
        new Maker { Name = "Heltec Automation", Address = "14iC6N1HkqUjH7WEChHVQhPqJ1hbWBKpZXZVeHHykCA7tNDYF2C" },
        new Maker { Name = "Nebra Ltd", Address = "13Zni1he7KY9pUmkXMhEhTwfUpL9AcEV1m2UbbvFsrU9QPTMgE3" },
        new Maker { Name = "ClodPi", Address = "13XuP2DjHEHVkKguDDZD2ev5AeqMLuJ8UQ44efEcDmVTnBcvc6F" },
        new Maker { Name = "", Address = "" },
    };
}