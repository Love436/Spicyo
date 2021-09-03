// <auto-genePriced />
using Spicyo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Spicyo.Migrations
{
    [DbContext(typeof(SpicyoContext))]
    partial class SpicyoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Spicyo.Models.Recipes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStPricegy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Recipe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecipeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("nvarchar(max)");
                    
                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });
            
            modelBuilder.Entity("Spicyo.Models.OrderedRecipes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStPricegy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Recipe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecipeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("nvarchar(max)");
                    
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OrderedRecipes");
                });

            modelBuilder.Entity("Spicyo.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStPricegy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");
                    
                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });


            modelBuilder.Entity("Spicyo.Models.Authenticate", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStPricegy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("LoginUser")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Password")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("Id");

                b.ToTable("Authenticate");
            });

            modelBuilder.Entity("Spicyo.Models.Authenticate", b =>
            {
                b.HasOne("Spicyo.Models.OrderedRecipes", "OrderedRecipes")
                    .WithMany()
                    .HasForeignKey("UserId");
            });


#pragma warning restore 612, 618
        }
    }
}
