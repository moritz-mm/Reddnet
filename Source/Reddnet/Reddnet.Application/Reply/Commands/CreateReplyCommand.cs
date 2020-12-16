﻿using MediatR;
using Reddnet.Application.Interfaces;
using Reddnet.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reddnet.Application.Reply.Commands
{
    public class CreateReplyCommand : IRequest<ReplyEntity>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }

    internal class CreateReplyHandler : IRequestHandler<CreateReplyCommand, ReplyEntity>
    {
        private readonly IDataContext context;

        public CreateReplyHandler(IDataContext context)
            => this.context = context;

        public async Task<ReplyEntity> Handle(CreateReplyCommand request, CancellationToken cancellationToken)
        {
            var reply = this.context.Replies.Add(new ReplyEntity
            {
                Id = Guid.NewGuid(),
                PostId = request.PostId,
                UserId = request.UserId,
                Content = request.Content
            });

            await this.context.SaveChangesAsync(cancellationToken);
            return reply.Entity;
        }
    }
}