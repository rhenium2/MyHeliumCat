using HeliumCat.Responses;

namespace HeliumCat;

public static class Constants
{
    public static class TransactionType
    {
        public const string RewardsV2 = "rewards_v2";
        public const string PocReceiptsV2 = "poc_receipts_v2";
        public const string StateChannelOpen = "state_channel_open_v1";
        public const string StateChannelClose = "state_channel_close_v1";
        public const string ValidatorHearbeat = "validator_heartbeat_v1";
        public const string PriceOracle = "price_oracle_v1";
        public const string AddGateway = "add_gateway_v1";
        public const string AssertLocationV2 = "assert_location_v2";
        public const string Payment = "payment_v1";
        public const string PaymentV2 = "payment_v2";
        public const string TransferHotspotV2 = "transfer_hotspot_v2";
        public const string Routing = "routing_v1";
        public const string ConsensusGroup = "consensus_group_v1";
        public const string ConsensusGroupFailure = "consensus_group_failure_v1";
        public const string Vars = "vars_v1";
    }

    // https://github.com/helium/explorer-api/blob/6d4236c753026ceab98326d1fd503a1ecab4c9be/src/helpers/makers.js#L22
    public static Maker[] Makers = new[]
    {
        new Maker
        {
            Name = "Helium Inc", Address = "14fzfjFcHpDR1rTH8BNPvSi5dKBbgxaDnmsVPbCjuq9ENjpZbxh"
        }, // Helium Inc (old)
        new Maker
        {
            Name = "Helium Inc", Address = "13daGGWvDQyTyHFDCPz8zDSVTWgPNNfJ4oh31Teec4TRWfjMx53"
        }, // Helium Inc (new)
        new Maker { Name = "Nebra Ltd", Address = "13Zni1he7KY9pUmkXMhEhTwfUpL9AcEV1m2UbbvFsrU9QPTMgE3" },
        new Maker { Name = "SyncroB.it", Address = "14rb2UcfS9U89QmKswpZpjRCUVCVu1haSyqyGY486EvsYtvdJmR" },
        new Maker { Name = "Bobcat", Address = "14sKWeeYWQWrBSnLGq79uRQqZyw3Ldi7oBdxbF6a54QboTNBXDL" },
        new Maker { Name = "LongAP", Address = "12zX4jgDGMbJgRwmCfRNGXBuphkQRqkUTcLzYHTQvd4Qgu8kiL4" },
        new Maker { Name = "RAKwireless", Address = "14h2zf1gEr9NmvDb2U53qucLN2jLrKU1ECBoxGnSnQ6tiT6V2kM" },
        new Maker { Name = "Kerlink", Address = "13Mpg5hCNjSxHJvWjaanwJPBuTXu1d4g5pGvGBkqQe3F8mAwXhK" },
        new Maker { Name = "DeWi Foundation", Address = "13LVwCqZEKLTVnf3sjGPY1NMkTE7fWtUVjmDfeuscMFgeK3f9pn" },
        new Maker { Name = "SenseCAP", Address = "14NBXJE5kAAZTMigY4dcjXSMG4CSqjYwvteQWwQsYhsu2TKN6AF" },
        new Maker { Name = "Pisces Miner", Address = "134C7Hn3vhfBLQZex4PVwtxQ2uPJH97h9YD2bhzy1W2XhMJyY6d" },
        new Maker { Name = "ClodPi", Address = "13XuP2DjHEHVkKguDDZD2ev5AeqMLuJ8UQ44efEcDmVTnBcvc6F" },
        new Maker { Name = "Linxdot", Address = "14eUfY1GsjK4WH6uZYoeagnFtigBKdvPruAXLmc5UsUMEDj3yib" },
        new Maker { Name = "COTX Networks", Address = "13cbbZXzqwp6YMM5JvAu5T1TRhenENEJVU5Q8vpLhunQYE1Acpp" },
        new Maker { Name = "Controllino", Address = "14go8hvEDnotWTyhYv6Hu5PTnRUAQzJqbB6dsDm1oThkCcZe9zd" },
        new Maker { Name = "Heltec Automation", Address = "14iC6N1HkqUjH7WEChHVQhPqJ1hbWBKpZXZVeHHykCA7tNDYF2C" },
        new Maker { Name = "FreedomFi", Address = "13y2EqUUzyQhQGtDSoXktz8m5jHNSiwAKLTYnHNxZq2uH5GGGym" },
        new Maker { Name = "PantherX", Address = "13v9iGhjvQUtVaZXcFFRCEbL1nPR4R8QJowBgMUcaGM2v1aV6mn" },
        new Maker { Name = "Hummingbird", Address = "14DdSjvEkBQ46xQ24LAtHwQkAeoUUZHfGCosgJe33nRQ6rZwPG3" },
        new Maker { Name = "RisingHF", Address = "143QF5dTGyAg1FqaE85mPApvjauFU8fUy6wavirQfJJDgSkC4gn" },
        new Maker { Name = "Browan/MerryIot", Address = "14iLrXUuGVhb7w1P8X3iBvRwoT9oemgbDonm5VVZJk56TdV8NV1" },
        new Maker { Name = "CalChip Connect", Address = "13ENbEQPAvytjLnqavnbSAzurhGoCSNkGECMx7eHHDAfEaDirdY" },
        new Maker { Name = "Milesight", Address = "13H6RpJojJjkqPRfzdrFBDbpYw4b3A6HyMxgGFRgXf31Nuwq4xJ" },
        new Maker { Name = "Deeper", Address = "146S5XCtvB1VwqYzJnRr8goiADUFTscGAUb2ihKUBVYuvMrrNCx" },
        new Maker { Name = "Midas", Address = "13eZdVcXajB3HrxKqeoPRPVPXbpqbDbaFpey5KfALk44VUphC5Z" },
        new Maker { Name = "Dragino", Address = "13gb8SJg8MZSyLakrx8ago146Gxk7tpvY4R3kgMSYGzAKE188m2" },
        new Maker { Name = "Pycom", Address = "14ZVsWQ8D98NCW673qxNepcYu1nfBiuuF6rhU2JHCFpfgqtnc5S" },
        new Maker { Name = "Atom", Address = "13GY3FdbfNpXoBkyRukFj2fx9851oqX2SUTKY8Gd4BDJsZsuYhd" },
        new Maker { Name = "KS Technologies", Address = "14BHX3FQUNnRZSXohHpwyYEEhymYaPxyDir4bJUPsCYa3czBJP4" },
        new Maker { Name = "EDA-IoT", Address = "136SdnfVaGvQn5uTrTMtcKBXeQueqP8vJUdQQR1nier1fhNK9Ny" },
        new Maker { Name = "Embit", Address = "14cJRHZan9fmUH4XpA8XD5YkqsEbWm2pqm524ewFMoRa8gzw3XB" },
        new Maker { Name = "Mimiq", Address = "13MS2kZHU4h6wp3tExgoHdDFjBsb9HB9JBvcbK9XmfNyJ7jqzVv" },
        new Maker { Name = "Dusun", Address = "14HQjuyvr8jj4ECp2jq9Y5riXZBhzekZRD7g28pvrYngXRzX368" },
        new Maker { Name = "Aitek Inc", Address = "14KKDX2N56h1xQEQVg6Um4myYUoG7Xes3P5scfwn9B5y5cY8e4t" },
        new Maker { Name = "Bobcat 5G", Address = "14gqqPV2HEs4PCNNUacKVG7XeAhCUkN553NcBVw4xfwSFcCjhXv" }
    };
}