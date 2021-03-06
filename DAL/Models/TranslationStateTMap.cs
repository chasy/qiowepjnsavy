﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Makersn.Models
{
    public class TranslationStateTMap : ClassMap<TranslationStateT>
    {
        public TranslationStateTMap()
        {
            Id(x => x.Gbn, "Gbn").GeneratedBy.Assigned();

            Map(x => x.Gbn, "Gbn").Nullable();
            Map(x => x.AllCnt, "AllCnt").Nullable();
            Map(x => x.DirectCnt, "DirectCnt").Nullable();
            Map(x => x.RequestCnt, "RequestCnt").Nullable();
            Map(x => x.CompleteCnt, "CompleteCnt").Nullable();
            Map(x => x.EnForKrCnt, "EnForKrCnt").Nullable();
            Map(x => x.KrForEnCnt, "KrForEnCnt").Nullable();
        }
    }
}
