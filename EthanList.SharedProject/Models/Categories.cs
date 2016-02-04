﻿using System;
using System.Collections.Generic;

namespace EthansList.Shared
{
    public static class Categories
    {
        static AllCategories catHelper = new AllCategories();

        public static List<KeyValuePair<string,string>> all { get { return catHelper.categories; } }
        public static List<CatTableGroup> Groups { get { return catHelper.CategoryGroups; } }
    }

    public class AllCategories
    {
        public List<KeyValuePair<string,string>> categories = new List<KeyValuePair<string,string>>()
        {
            { new KeyValuePair<string, string>("/search/apa", "apts / housing")},
            { new KeyValuePair<string, string>("/search/swp", "housing swap")},
            { new KeyValuePair<string, string>("/search/hsw", "housing wanted")},
            { new KeyValuePair<string, string>("/search/off", "office / commercial")},
            { new KeyValuePair<string, string>("/search/prk", "parking / storage")},
            { new KeyValuePair<string, string>("/search/rea", "real estate for sale")},
            { new KeyValuePair<string, string>("/search/roo", "rooms / shared")},
            { new KeyValuePair<string, string>("/search/sha", "rooms wanted")},
            { new KeyValuePair<string, string>("/search/sub", "sublets / temporary")},
            { new KeyValuePair<string, string>("/search/vac", "vacation rentals")},
            { new KeyValuePair<string, string>("/search/ata", "antiques")},
            { new KeyValuePair<string, string>("/search/ppa", "appliances")},
            { new KeyValuePair<string, string>("/search/ara", "arts+crafts")},
            { new KeyValuePair<string, string>("/search/sna", "atv/utv/sno")},
            { new KeyValuePair<string, string>("/i/auto_parts", "auto parts")},
            { new KeyValuePair<string, string>("/search/baa", "baby+kid")},
            { new KeyValuePair<string, string>("/search/bar", "barter")},
            { new KeyValuePair<string, string>("/search/haa", "beauty+hlth")},
            { new KeyValuePair<string, string>("/i/bikes", "bikes")},
            { new KeyValuePair<string, string>("/i/boats", "boats")},
            { new KeyValuePair<string, string>("/search/bka", "books")},
            { new KeyValuePair<string, string>("/search/bfa", "business")},
            { new KeyValuePair<string, string>("/i/autos", "cars+trucks")},
            { new KeyValuePair<string, string>("/search/ema", "cds/dvd/vhs")},
            { new KeyValuePair<string, string>("/search/moa", "cell phones")},
            { new KeyValuePair<string, string>("/search/cla", "clothes+acc")},
            { new KeyValuePair<string, string>("/search/cba", "collectibles")},
            { new KeyValuePair<string, string>("/i/computers", "computers")},
            { new KeyValuePair<string, string>("/search/ela", "electronics")},
            { new KeyValuePair<string, string>("/search/gra", "farm+garden")},
            { new KeyValuePair<string, string>("/search/zip", "free")},
            { new KeyValuePair<string, string>("/search/fua", "furniture")},
            { new KeyValuePair<string, string>("/search/gms", "garage sale")},
            { new KeyValuePair<string, string>("/search/foa", "general")},
            { new KeyValuePair<string, string>("/search/hva", "heavy equip")},
            { new KeyValuePair<string, string>("/search/hsa", "household")},
            { new KeyValuePair<string, string>("/search/jwa", "jewelry")},
            { new KeyValuePair<string, string>("/search/maa", "materials")},
            { new KeyValuePair<string, string>("/i/motorcycles", "motorcycles")},
            { new KeyValuePair<string, string>("/search/msa", "music instr")},
            { new KeyValuePair<string, string>("/search/pha", "photo+video")},
            { new KeyValuePair<string, string>("/search/rva", "rvs+camp")},
            { new KeyValuePair<string, string>("/search/sga", "sporting")},
            { new KeyValuePair<string, string>("/search/tia", "tickets")},
            { new KeyValuePair<string, string>("/search/tla", "tools")},
            { new KeyValuePair<string, string>("/search/taa", "toys+games")},
            { new KeyValuePair<string, string>("/search/vga", "video gaming")},
            { new KeyValuePair<string, string>("/search/waa", "wanted")},
            { new KeyValuePair<string, string>("/search/aos", "automotive")},
            { new KeyValuePair<string, string>("/search/bts", "beauty")},
            { new KeyValuePair<string, string>("/search/cps", "computer")},
            { new KeyValuePair<string, string>("/search/crs", "creative")},
            { new KeyValuePair<string, string>("/search/cys", "cycle")},
            { new KeyValuePair<string, string>("/search/evs", "event")},
            { new KeyValuePair<string, string>("/search/fgs", "farm+garden")},
            { new KeyValuePair<string, string>("/search/fns", "financial")},
            { new KeyValuePair<string, string>("/search/hss", "household")},
            { new KeyValuePair<string, string>("/search/lbs", "labor/move")},
            { new KeyValuePair<string, string>("/search/lgs", "legal")},
            { new KeyValuePair<string, string>("/search/lss", "lessons")},
            { new KeyValuePair<string, string>("/search/mas", "marine")},
            { new KeyValuePair<string, string>("/search/pas", "pet")},
            { new KeyValuePair<string, string>("/search/rts", "real estate")},
            { new KeyValuePair<string, string>("/search/sks", "skill'd trade")},
            { new KeyValuePair<string, string>("/search/biz", "sm biz ads")},
            { new KeyValuePair<string, string>("/search/thp", "therapeutic")},
            { new KeyValuePair<string, string>("/search/trv", "travel/vac")},
            { new KeyValuePair<string, string>("/search/wet", "write/ed/tr8")},
            { new KeyValuePair<string, string>("/search/acc", "accounting+finance")},
            { new KeyValuePair<string, string>("/search/ofc", "admin / office")},
            { new KeyValuePair<string, string>("/search/egr", "arch / engineering")},
            { new KeyValuePair<string, string>("/search/med", "art / media / design")},
            { new KeyValuePair<string, string>("/search/sci", "biotech / science")},
            { new KeyValuePair<string, string>("/search/bus", "business / mgmt")},
            { new KeyValuePair<string, string>("/search/csr", "customer service")},
            { new KeyValuePair<string, string>("/search/edu", "education")},
            { new KeyValuePair<string, string>("/search/fbh", "food / bev / hosp")},
            { new KeyValuePair<string, string>("/search/lab", "general labor")},
            { new KeyValuePair<string, string>("/search/gov", "government")},
            { new KeyValuePair<string, string>("/search/hum", "human resources")},
            { new KeyValuePair<string, string>("/search/eng", "internet engineers")},
            { new KeyValuePair<string, string>("/search/lgl", "legal / paralegal")},
            { new KeyValuePair<string, string>("/search/mnu", "manufacturing")},
            { new KeyValuePair<string, string>("/search/mar", "marketing / pr / ad")},
            { new KeyValuePair<string, string>("/search/hea", "medical / health")},
            { new KeyValuePair<string, string>("/search/npo", "nonprofit sector")},
            { new KeyValuePair<string, string>("/search/rej", "real estate")},
            { new KeyValuePair<string, string>("/search/ret", "retail / wholesale")},
            { new KeyValuePair<string, string>("/search/sls", "sales / biz dev")},
            { new KeyValuePair<string, string>("/search/spa", "salon / spa / fitness")},
            { new KeyValuePair<string, string>("/search/sec", "security")},
            { new KeyValuePair<string, string>("/search/trd", "skilled trade / craft")},
            { new KeyValuePair<string, string>("/search/sof", "software / qa / dba")},
            { new KeyValuePair<string, string>("/search/sad", "systems / network")},
            { new KeyValuePair<string, string>("/search/tch", "technical support")},
            { new KeyValuePair<string, string>("/search/trp", "transport")},
            { new KeyValuePair<string, string>("/search/tfr", "tv / film / video")},
            { new KeyValuePair<string, string>("/search/web", "web / info design")},
            { new KeyValuePair<string, string>("/search/wri", "writing / editing")},
            { new KeyValuePair<string, string>("/search/etc", "[ETC]")},
            { new KeyValuePair<string, string>("/search/jjj?employment_type=2", "[ part-time ]")},
            { new KeyValuePair<string, string>("/search/cpg", "computer")},
            { new KeyValuePair<string, string>("/search/crg", "creative")},
            { new KeyValuePair<string, string>("/search/cwg", "crew")},
            { new KeyValuePair<string, string>("/search/dmg", "domestic")},
            { new KeyValuePair<string, string>("/search/evg", "event")},
            { new KeyValuePair<string, string>("/search/lbg", "labor")},
            { new KeyValuePair<string, string>("/search/tlg", "talent")},
            { new KeyValuePair<string, string>("/search/wrg", "writing")},
        };


        public List<CatTableGroup> CategoryGroups = new List<CatTableGroup>()
        { 
                new CatTableGroup()
                    {
                        Name = "Housing",
                        Items = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>("/search/apa", "apts / housing"),
                                new KeyValuePair<string, string>("/search/swp", "housing swap"),
                                new KeyValuePair<string, string>("/search/hsw", "housing wanted"),
                                new KeyValuePair<string, string>("/search/off", "office / commercial"),
                                new KeyValuePair<string, string>("/search/prk", "parking / storage"),
                                new KeyValuePair<string, string>("/search/rea", "real estate for sale"),
                                new KeyValuePair<string, string>("/search/roo", "rooms / shared"),
                                new KeyValuePair<string, string>("/search/sha", "rooms wanted"),
                                new KeyValuePair<string, string>("/search/sub", "sublets / temporary"),
                                new KeyValuePair<string, string>("/search/vac", "vacation rentals"),
                            },
                    },
                new CatTableGroup()
                    {
                        Name = "For Sale",
                        Items = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("/search/ata", "antiques"),
                            new KeyValuePair<string, string>("/search/ppa", "appliances"),
                            new KeyValuePair<string, string>("/search/ara", "arts+crafts"),
                            new KeyValuePair<string, string>("/search/sna", "atv/utv/sno"),
                            new KeyValuePair<string, string>("/i/auto_parts", "auto parts"),
                            new KeyValuePair<string, string>("/search/baa", "baby+kid"),
                            new KeyValuePair<string, string>("/search/bar", "barter"),
                            new KeyValuePair<string, string>("/search/haa", "beauty+hlth"),
                            new KeyValuePair<string, string>("/i/bikes", "bikes"),
                            new KeyValuePair<string, string>("/i/boats", "boats"),
                            new KeyValuePair<string, string>("/search/bka", "books"),
                            new KeyValuePair<string, string>("/search/bfa", "business"),
                            new KeyValuePair<string, string>("/i/autos", "cars+trucks"),
                            new KeyValuePair<string, string>("/search/ema", "cds/dvd/vhs"),
                            new KeyValuePair<string, string>("/search/moa", "cell phones"),
                            new KeyValuePair<string, string>("/search/cla", "clothes+acc"),
                            new KeyValuePair<string, string>("/search/cba", "collectibles"),
                            new KeyValuePair<string, string>("/i/computers", "computers"),
                            new KeyValuePair<string, string>("/search/ela", "electronics"),
                            new KeyValuePair<string, string>("/search/gra", "farm+garden"),
                            new KeyValuePair<string, string>("/search/zip", "free"),
                            new KeyValuePair<string, string>("/search/fua", "furniture"),
                            new KeyValuePair<string, string>("/search/gms", "garage sale"),
                            new KeyValuePair<string, string>("/search/foa", "general"),
                            new KeyValuePair<string, string>("/search/hva", "heavy equip"),
                            new KeyValuePair<string, string>("/search/hsa", "household"),
                            new KeyValuePair<string, string>("/search/jwa", "jewelry"),
                            new KeyValuePair<string, string>("/search/maa", "materials"),
                            new KeyValuePair<string, string>("/i/motorcycles", "motorcycles"),
                            new KeyValuePair<string, string>("/search/msa", "music instr"),
                            new KeyValuePair<string, string>("/search/pha", "photo+video"),
                            new KeyValuePair<string, string>("/search/rva", "rvs+camp"),
                            new KeyValuePair<string, string>("/search/sga", "sporting"),
                            new KeyValuePair<string, string>("/search/tia", "tickets"),
                            new KeyValuePair<string, string>("/search/tla", "tools"),
                            new KeyValuePair<string, string>("/search/taa", "toys+games"),
                            new KeyValuePair<string, string>("/search/vga", "video gaming"),
                            new KeyValuePair<string, string>("/search/waa", "wanted"),
                        },
                    },
                new CatTableGroup()
                {
                    Name = "Services",
                    Items = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("/search/aos", "automotive"),
                        new KeyValuePair<string, string>("/search/bts", "beauty"),
                        new KeyValuePair<string, string>("/search/cps", "computer"),
                        new KeyValuePair<string, string>("/search/crs", "creative"),
                        new KeyValuePair<string, string>("/search/cys", "cycle"),
                        new KeyValuePair<string, string>("/search/evs", "event"),
                        new KeyValuePair<string, string>("/search/fgs", "farm+garden"),
                        new KeyValuePair<string, string>("/search/fns", "financial"),
                        new KeyValuePair<string, string>("/search/hss", "household"),
                        new KeyValuePair<string, string>("/search/lbs", "labor/move"),
                        new KeyValuePair<string, string>("/search/lgs", "legal"),
                        new KeyValuePair<string, string>("/search/lss", "lessons"),
                        new KeyValuePair<string, string>("/search/mas", "marine"),
                        new KeyValuePair<string, string>("/search/pas", "pet"),
                        new KeyValuePair<string, string>("/search/rts", "real estate"),
                        new KeyValuePair<string, string>("/search/sks", "skill'd trade"),
                        new KeyValuePair<string, string>("/search/biz", "sm biz ads"),
                        new KeyValuePair<string, string>("/search/thp", "therapeutic"),
                        new KeyValuePair<string, string>("/search/trv", "travel/vac"),
                        new KeyValuePair<string, string>("/search/wet", "write/ed/tr8"),
                    },
                },
                new CatTableGroup()
                {
                    Name = "Jobs",
                    Items = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("/search/acc", "accounting+finance"),
                        new KeyValuePair<string, string>("/search/ofc", "admin / office"),
                        new KeyValuePair<string, string>("/search/egr", "arch / engineering"),
                        new KeyValuePair<string, string>("/search/med", "art / media / design"),
                        new KeyValuePair<string, string>("/search/sci", "biotech / science"),
                        new KeyValuePair<string, string>("/search/bus", "business / mgmt"),
                        new KeyValuePair<string, string>("/search/csr", "customer service"),
                        new KeyValuePair<string, string>("/search/edu", "education"),
                        new KeyValuePair<string, string>("/search/fbh", "food / bev / hosp"),
                        new KeyValuePair<string, string>("/search/lab", "general labor"),
                        new KeyValuePair<string, string>("/search/gov", "government"),
                        new KeyValuePair<string, string>("/search/hum", "human resources"),
                        new KeyValuePair<string, string>("/search/eng", "internet engineers"),
                        new KeyValuePair<string, string>("/search/lgl", "legal / paralegal"),
                        new KeyValuePair<string, string>("/search/mnu", "manufacturing"),
                        new KeyValuePair<string, string>("/search/mar", "marketing / pr / ad"),
                        new KeyValuePair<string, string>("/search/hea", "medical / health"),
                        new KeyValuePair<string, string>("/search/npo", "nonprofit sector"),
                        new KeyValuePair<string, string>("/search/rej", "real estate"),
                        new KeyValuePair<string, string>("/search/ret", "retail / wholesale"),
                        new KeyValuePair<string, string>("/search/sls", "sales / biz dev"),
                        new KeyValuePair<string, string>("/search/spa", "salon / spa / fitness"),
                        new KeyValuePair<string, string>("/search/sec", "security"),
                        new KeyValuePair<string, string>("/search/trd", "skilled trade / craft"),
                        new KeyValuePair<string, string>("/search/sof", "software / qa / dba"),
                        new KeyValuePair<string, string>("/search/sad", "systems / network"),
                        new KeyValuePair<string, string>("/search/tch", "technical support"),
                        new KeyValuePair<string, string>("/search/trp", "transport"),
                        new KeyValuePair<string, string>("/search/tfr", "tv / film / video"),
                        new KeyValuePair<string, string>("/search/web", "web / info design"),
                        new KeyValuePair<string, string>("/search/wri", "writing / editing"),
                        new KeyValuePair<string, string>("/search/etc", "[ETC]"),
                        new KeyValuePair<string, string>("/search/jjj?employment_type=2", "[ part-time ]"),
                    },
                },
                new CatTableGroup()
                {
                    Name = "Gigs",
                    Items = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("/search/cpg", "computer"),
                        new KeyValuePair<string, string>("/search/crg", "creative"),
                        new KeyValuePair<string, string>("/search/cwg", "crew"),
                        new KeyValuePair<string, string>("/search/dmg", "domestic"),
                        new KeyValuePair<string, string>("/search/evg", "event"),
                        new KeyValuePair<string, string>("/search/lbg", "labor"),
                        new KeyValuePair<string, string>("/search/tlg", "talent"),
                        new KeyValuePair<string, string>("/search/wrg", "writing"),
                    },
                },
                

        };
         

    }

    public class CatTableGroup
    {
        public string Name { get; set; }

        public List<KeyValuePair<string,string>> Items
        {
            get { return items; }
            set { items = value; }
        }
        protected List<KeyValuePair<string, string>> items = new List<KeyValuePair<string,string>> ();
    }

//    public class CatTableItem
//    {
//        public KeyValuePair<string, string> Cat
//        {
//            get { return cat; }
//            set { cat = value; }
//        }
//        protected KeyValuePair<string, string> cat = new KeyValuePair<string, string>();
//    }

}

