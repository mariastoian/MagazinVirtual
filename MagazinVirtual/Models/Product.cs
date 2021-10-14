using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazinVirtual.Models
{
    public class Product
    {
        public string Denumire { get; set; }
        public int Id { get; set; }
        public string Camp { get; set; }
        public List<mCategorieCuProduse> mCategoriiCuProduse { get; set; }
        public CategorieCuProduse CategCuProduse { get; set; }
    }

    public class CategoriiArticole
    {
        public List<mCategorieCuProduse> ListaArticolePeNivele { get; set; }
        public CategoriiArticole(MagazinVirtualEntities de, string denumire)
        {
            ListaArticolePeNivele = new List<mCategorieCuProduse>();
            int nivel = 0;
            List<ProductTypes> categorii = de.ProductTypes.Where(x => x.Id_Parinte == null).OrderBy(x => x.Name).ToList();
            foreach (ProductTypes categ in categorii)
            {
                ListaArticolePeNivele.Add(new mCategorieCuProduse(categ, nivel, denumire));
            }
            nivel += 1;
            categorii.Clear();
            foreach (mCategorieCuProduse mCP in ListaArticolePeNivele.Where(x => x.Nivel == 0).ToList())
            {
                foreach (ProductTypes categ in de.ProductTypes.Where(x => x.Id_Parinte == mCP.IdCategorie).OrderBy(x => x.Name))
                {
                    ListaArticolePeNivele.Add(new mCategorieCuProduse(categ, nivel, denumire));
                }
            }
            nivel += 2;
            foreach (mCategorieCuProduse mCP in ListaArticolePeNivele.Where(x => x.Nivel == 1).ToList())
            {
                foreach (ProductTypes categ in de.ProductTypes.Where(x => x.Id_Parinte == mCP.IdCategorie).OrderBy(x => x.Name))
                {
                    ListaArticolePeNivele.Add(new mCategorieCuProduse(categ, nivel, denumire));
                }
            }
            nivel += 3;
            foreach (mCategorieCuProduse mCP in ListaArticolePeNivele.Where(x => x.Nivel == 2).ToList())
            {
                foreach (ProductTypes categ in de.ProductTypes.Where(x => x.Id_Parinte == mCP.IdCategorie).OrderBy(x => x.Name))
                {
                    ListaArticolePeNivele.Add(new mCategorieCuProduse(categ, nivel, denumire));
                }
            }
        }
    }

    public class mCategorieCuProduse
    {
        public int IdCategorie { get; set; }
        public int? IdParinte { get; set; }
        public string DenumireCategorie { get; set; }
        public int Nivel { get; set; }
        public int NrProduse { get; set; }
        public mCategorieCuProduse(ProductTypes categorieCuProduse, int nivel, string denumire)
        {
            IdCategorie = categorieCuProduse.Id;
            IdParinte = categorieCuProduse.Id_Parinte;
            DenumireCategorie = categorieCuProduse.Name.Trim();
            Nivel = nivel;
            NrProduse = categorieCuProduse.Products.Where(x => x.Name.ToLower().Contains(denumire)).Count();
        }
    }
    public class CategorieCuProduse
    {
        public int IdCategorie { get; set; }
        public int PageNr { get; set; }
        public string DenumireCategorie { get; set; }
        public IPagedList<Produs> ProduseInCategorie { get; set; }
        public CategorieCuProduse(MagazinVirtualEntities ve, string den, int idCat, int pagenr)
        {
            IOrderedQueryable<Products> queryResult = null;
            if (idCat != 0)
                queryResult = ve.Products.Where(x => (x.Name.Contains(den) || x.ProductTypes.Name.Contains(den))
                                                        && x.ProductTypeId == idCat /*&& x.Tip != "S"*/).OrderBy(x => x.Name).ThenBy(y => y.Id);
            else
                queryResult = ve.Products.Where(x => (x.Name.Contains(den) || x.ProductTypes.Name.Contains(den))
                                                        /*&& x.Tip != "S"*/).OrderBy(x => x.Name).ThenBy(y => y.Id);
            // to preserve order (case insesitive ordering within the database!)
            IPagedList<Products> produsePePaginaCurenta = queryResult.ToPagedList(pagenr, 8);
            List<Produs> listprod = new List<Produs>();
            foreach (var art in queryResult.ToList())
            {
                //if (IdPorduse.Contains(art.ID))
                if (produsePePaginaCurenta.Contains(art))
                {
                    listprod.Add(new Produs(art, ve));
                }
                else
                {
                    listprod.Add(null);
                }
            }
            ProduseInCategorie = listprod.ToPagedList(pagenr, 8);
            IdCategorie = idCat;
            DenumireCategorie = idCat == 0 ? "Toate" : ve.ProductTypes.Where(x => x.Id == idCat).Select(x => x.Name).First().Trim();
            PageNr = pagenr;
        }
    }
    

    public class Produs
    {
        public int IdProdus { get; set; }
        public int IdCategorieProdus { get; set; }
        public string DenumireCategorieProdus { get; set; }
        public string DenumireProdus { get; set; }
        public string ImgSrc { get; set; }
        public bool Activ { get; set; }
        public decimal Price { get; set; }
        public string Mentiuni { get; set; }
        public Produs(Products art, MagazinVirtualEntities de)
        {
            IdProdus = art.Id;
            IdCategorieProdus = art.ProductTypeId;

            DenumireProdus = art.Name.Trim();
            DenumireCategorieProdus = art.ProductTypes.Name.Trim();
            Price = art.Price;
            Activ = art.IsAvailable;
            Mentiuni = art.Description;
            if (art.ImagePath != null)
            {
                try
                {
                    ImgSrc = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(art.ImagePath));
                }
                catch (Exception ex)
                {
                    ImgSrc = "~/Content/images/faraimg.jpg";
                }
            }
            else
                ImgSrc = "~/Content/images/faraimg.jpg";

        }
       
    }
    public class Categorii
    {
        public List<ParinteCategorie> ListaCategori { get; set; }
        public Categorii(MagazinVirtualEntities de)
        {
            List<ParinteCategorie> listaCategori = new List<ParinteCategorie>();
            foreach (ProductTypes categ in de.ProductTypes)
            {

                listaCategori.Add(new ParinteCategorie(categ, de));
            }
            ListaCategori = listaCategori;
        }
    }
    public class ParinteCategorie
    {
        public int IdCategorie { get; set; }
        public string IdParinteCategorie { get; set; }
        public string NumeParinteCategorie { get; set; }
        public string NumeCategorie { get; set; }
        public int ProduseContinute { get; set; }
        public ParinteCategorie(ProductTypes cat, MagazinVirtualEntities de)
        {
            IdCategorie = cat.Id;
            NumeCategorie = cat.Name;
            ProduseContinute = cat.Products.Count();
            if (cat.Id_Parinte != null)
            {
                IdParinteCategorie = cat.Id_Parinte.ToString();
                NumeParinteCategorie = de.ProductTypes.FirstOrDefault(x => x.Id == cat.Id_Parinte).Name;
            }
            else
            {
                IdParinteCategorie = "";
                NumeParinteCategorie = "";
            }
        }
    }


    public class PostCreareCategorie
    {
        public string Denumire { get; set; }
        public int IdParinte { get; set; }
    }

    public class GetCreareProdus
    {
        public List<ParinteCategorie> CategoriiProdus { get; set; }

        public string CodProdusRecomandat { get; set; }
        public GetCreareProdus(MagazinVirtualEntities de)
        {

            List<ParinteCategorie> listaCategorii = new List<ParinteCategorie>();
            foreach (ProductTypes categ in de.ProductTypes)
            {
                listaCategorii.Add(new ParinteCategorie(categ, de));
            }
            CategoriiProdus = listaCategorii;
        }
    }

    public class PostCreareProdus
    {
        public string DenumireProdus { get; set; }
        public int IdCategorie { get; set; }
        public decimal Price { get; set; }
        public string Mentiuni { get; set; }
    }


    public class GetEditProdus
    {
        public List<ParinteCategorie> CategoriiProdus { get; set; }

        public Products Art { get; set; }
        public GetEditProdus(MagazinVirtualEntities de, int idArt)
        {
            //categorii pentru produs
            List<ParinteCategorie> listaCategorii = new List<ParinteCategorie>();
            foreach (ProductTypes categ in de.ProductTypes)
            {
                listaCategorii.Add(new ParinteCategorie(categ, de));
            }
            CategoriiProdus = listaCategorii;
            Art = de.Products.Where(x => x.Id == idArt).FirstOrDefault();


        }
    }


    public class PostEditProdus
    {
        public int IdProdus { get; set; }
        public string DenumireProdus { get; set; }
        public int IdCategorie { get; set; }
        public decimal Price { get; set; }
        public string Mentiuni { get; set; }
    }

    public class PostUploadImagine
    {
        public int Id { get; set; }
        public HttpPostedFileWrapper ImageFile { get; set; }
    }

}