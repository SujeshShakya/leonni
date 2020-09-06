using System.Collections.Generic;
using Leonni.Models.ViewModels;
using System;

namespace Leonni.Models.ModelMappers
{
    public static class MessageMap
    {
        public static Message Map(MessageModel objModel)
        {
            return new Message
            {
                Id = objModel.Id,
               SentBy = objModel.SentBy,
               SentDate = objModel.SentDate,
               MessageContent = objModel.MessageContent,
               SentTo = objModel.SentTo
            
            };

        }

        public static MessageModel Map(Message objModel)
        {
            return new MessageModel
            {
                Id = objModel.Id,
                SentBy = objModel.SentBy,
                SentDate = objModel.SentDate,
                MessageContent = objModel.MessageContent,
                SentTo = objModel.SentTo
            };
        }

        public static List<MessageModel> Map(List<Message> lstModel)
        {
            var ret = new List<MessageModel>();
            int i = 0, lstModel_count = lstModel.Count;
            
            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(MessageMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
