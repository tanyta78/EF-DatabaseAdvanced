namespace BusTicketSystem.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ReviewService:IReviewService
    {
        private readonly BusTicketSystemContext db;

        public ReviewService(BusTicketSystemContext db)
        {
            this.db = db;
        }

        public string PublishReview(int customerId, double grade, string busCompanyName, string content)
        {
            var customer = this.db.Customers
               .FirstOrDefault(c => c.Id == customerId);
            var company = this.db.BusCompanies.FirstOrDefault(c => c.Name == busCompanyName);

            if (customer==null||company==null)
            {
                throw new ArgumentException("No such customer or company in db!");
            }

            var review = new Review
            {
                BusCompany = company,
                Content = content,
                Grade = grade,
                Customer = customer
            };

            this.db.Reviews.Add(review);
            this.db.SaveChanges();

            var result = $"Customer {customer.FullName} published review for company {busCompanyName}";
            
            return result;
        }

        public string PrintReview(int busCompanyId)
        {
            var company = this.db.BusCompanies
                .Include(c=>c.CompanyReviews)
                .ThenInclude(r=>r.Customer)
                .FirstOrDefault(c => c.Id == busCompanyId);

            if (company == null)
            {
                throw new ArgumentException("No such company in db!");
            }

            var reviews = company.CompanyReviews.Select(r =>
                $"{r.Id} {r.Grade} {r.PublishingDate}" + Environment.NewLine + $"{r.Customer.FullName}" +
                Environment.NewLine + $"{r.Content ?? ""}").ToList();

            if (reviews.Count==0)
            {
                return $"No reviews for {company.Name}";
            }

            var result=String.Join(Environment.NewLine,reviews);

            return result;
        }
    }
}
