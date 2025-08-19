using BlogsAPI.Data;
using BlogsAPI.Models;
using System;
using System.Collections.Generic;

namespace BlogsAPI.Services
{

    public interface ICommentsService {

        /// <summary>
        ///   this interface defines the contract for the CommentsService.
        ///   all methods are designed to handle CRUD operations for comments as required in the description

        /// Gets all comments.
        public IEnumerable<Comment> GetAllComments();

        public Comment GetCommentById(int id);  
        public Comment AddComment(AddCommentDTO commentDto);
        public Comment UpdateComment(int id,UpdateCommentDTO commentDto);
        public void DeleteComment(int id);
        public IEnumerable<Comment> GetCommentsByPostId(int postId);
        public IEnumerable<Comment> GetCommentsByAuthorId(int authorId);

    }


    public class CommentsService : ICommentsService
    {
        private readonly BlogsDbContext _context;
        public CommentsService(BlogsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comments;
        }

        public Comment GetCommentById(int id)
        {
            return _context.Comments.Find(id) ?? throw new KeyNotFoundException($"Comment with ID {id} not found.");
        }
        public Comment AddComment(AddCommentDTO commentDto)
        {
            if (commentDto == null) throw new ArgumentNullException(nameof(commentDto));
            var comment = new Comment
            {
                Content = commentDto.Content,
                AuthorId = commentDto.AuthorId,
                PostId = commentDto.PostId,
                CreatedDate = DateTime.Now
            };
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return comment;
        }
        public Comment UpdateComment(int id ,UpdateCommentDTO commentDto)
        {
            if (commentDto == null) throw new ArgumentNullException(nameof(commentDto));
            var comment = _context.Comments.Find(commentDto.Id);
            if (comment == null) throw new KeyNotFoundException($"Comment with ID {commentDto.Id} not found.");
            comment.Content = commentDto.Content;
            _context.SaveChanges();
            return comment;
        }
        public void DeleteComment(int id) { 
            var comment = _context.Comments.Find(id);
            if (comment == null) throw new KeyNotFoundException($"Comment with ID {id} not found.");
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }

        public IEnumerable<Comment> GetCommentsByPostId(int postId)
        {
            return _context.Comments.Where(c => c.PostId == postId);
        }
        public IEnumerable<Comment> GetCommentsByAuthorId(int authorId)
        {
            return _context.Comments.Where(c => c.AuthorId == authorId);
        }

    }
}
