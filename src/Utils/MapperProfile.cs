using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.DTO;
using src.Entity;
using static src.DTO.AddressDTO;
using static src.DTO.CartDTO;
using static src.DTO.CategoryDTO;
using static src.DTO.GemstonesDTO;
using static src.DTO.GymDTO;
using static src.DTO.JewelryDTO;
using static src.DTO.OrderDTO;
using static src.DTO.OrderGemstoneDTO;
using static src.DTO.PaymentDTO;
using static src.DTO.ReviewDTO;
using static src.DTO.UserDTO;

namespace src.Utils
{
    /// <summary>
    /// MapperProfile class is typically used to:
    /// mappings between entity classes and DTOs (Data Transfer Objects)
    /// </summary>

    public class MapperProfile : Profile // MapperProfile inherits from Profile
    {
        // constructor
        public MapperProfile()
        {
            // category
            // Mapping configurations between entity classes and DTOs
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            // Mapping from CategoryUpdateDto to Category with a condition to map properties only if they are not null
            CreateMap<CategoryUpdateDto, Category>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );

            // review
            // Mapping configurations between entity classes and DTOs
            CreateMap<Review, ReviewReadDTO>();
            CreateMap<ReviewCreateDTO, Review>();
            // Mapping from ReviewUpdateDTO to Review with a condition to map properties only if they are not null
            CreateMap<ReviewUpdateDTO, Review>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );

            // cart
            // Mapping configurations between entity classes and DTOs
            CreateMap<Cart, CartReadDTO>();
            CreateMap<CartCreateDTO, Cart>();
            // Mapping from CartUpdateDTO to Cart with a condition to map properties only if they are not null
            CreateMap<CartUpdateDTO, Cart>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );
            ///users
            // Mapping configurations between entity classes and DTOs
            CreateMap<Users, UserReadDto>();
            CreateMap<UserCreateDto, Users>();

            // Mapping from UserUpdateDto to Users with a condition to map properties only if they are not null

            // Additional mappings
            CreateMap<UserLoginDto, Users>();
            CreateMap<PasswordUpdateDto, Users>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AdminUpdateDto, Users>();

            CreateMap<Address, AddressReadDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId.ToString())); // Con

            CreateMap<Users, UserProfileDto>();
            ///Address
            CreateMap<AddressCreateDto, Address>();

            // Mapping from AddressUpdateDto to Address with a condition to map properties only if they are not null
            CreateMap<AddressUpdateDto, Address>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Payment, PaymentReadDto>();
            CreateMap<PaymentCreateDto, Payment>();
            // Mapping from PaymentCard to Payment with a condition to map properties only if they are not null
            CreateMap<PaymentUpdateDto, Payment>()
                .ForAllMembers(options =>
                    options.Condition((src, dest, srcProperty) => srcProperty != null)
                );

            //Gemstones mappings
            CreateMap<Gemstones, GemstoneReadDto>();
            CreateMap<GemstoneCreateDto, Gemstones>();
            // Creates a mapping from GemstoneUpdateDto to Gemstones and applies a condition to map only non-null members
            CreateMap<GemstoneUpdateDto, Gemstones>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            //Jewelry mappings
            CreateMap<Jewelry, JewelryDTO.JewelryReadDto>();
            CreateMap<JewelryCreateDto, Jewelry>();
            // Creates a mapping from JewelryUpdateDto to Jewelry and applies a condition to map only non-null members
            CreateMap<JewelryUpdateDto, Jewelry>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // Order mappings
            CreateMap<Order, OrderReadDto>();
            CreateMap<OrderCreateDto, Order>();
            // Mapping from OrderUpdateDto to Order with a condition to map properties only if they are not null
            CreateMap<OrderUpdateDto, Order>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // OrderGemstone mappings

            CreateMap<OrderGemstone, OrderGemstoneReadDto>();
            CreateMap<OrderGemstoneCreateDto, OrderGemstone>();
            // Mapping from OrderGemstoneUpdateDto to OrderGemstone with a condition to map properties only if they are not null
            CreateMap<OrderGemstoneUpdateDto, OrderGemstone>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

                 // OrderGemstone mappings

            CreateMap<Gym, GymReadDto>();
            CreateMap<GymCreateDto, Gym>();
            // Mapping from GymUpdateDto to Gym with a condition to map properties only if they are not null
            CreateMap<GymUpdateDto, Gym>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    } // end class
} // end namespace
