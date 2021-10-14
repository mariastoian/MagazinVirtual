using MagazinVirtual.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazinVirtual.Controllers
{
    public class ProduseController : Controller
    {
        // GET: Produse
        private MagazinVirtualEntities db = new MagazinVirtualEntities();
        public ActionResult Index()
        {
            CategoriiArticole CatArt = new CategoriiArticole(db, "");
            return View(CatArt);
        }
        public ActionResult FiltrareArticole(string denumire = "")
        {
            CategoriiArticole CatArt = new CategoriiArticole(db, denumire);
            return PartialView("~/Views/Produse/_ListaCategoriiProduse", CatArt);
        }
        public ActionResult ViewProduseDinCateg(int page, int IdCategorie, string denumire)
        {
            CategorieCuProduse ccp = new CategorieCuProduse(db, denumire, IdCategorie, page);
            return PartialView("~/Views/Produse/_MainCategorieCuProduse.cshtml", ccp);
        }

        [HttpGet]
        public ActionResult CreareCategorieProdus()
        {
            Categorii cat = new Categorii(db);
            return PartialView("~/Views/Produse/_CreareCategorie.cshtml", cat);

        }

        [HttpPost]
        public ActionResult CreareCategorieProdus(string numeCategorie, int idParinteCategorie)
        {
            ProductTypes newCat = new ProductTypes();
            newCat.Name = numeCategorie;
            if (idParinteCategorie != 0)
                newCat.Id_Parinte = idParinteCategorie;
            db.ProductTypes.Add(newCat);
            db.SaveChanges();
            return PartialView("~/Views/Produse/_CreareCategorie.cshtml", new Categorii(db));

        }

        [HttpGet]
        public ActionResult CreareProdus()
        {
            GetCreareProdus newProd = new GetCreareProdus(db);

            return PartialView("~/Views/Produse/_CreareProdus.cshtml", newProd);

        }

        [HttpPost]
        public ActionResult CreareProdus(PostCreareProdus prod)
        {
            Products newArt = new Products();
            newArt.ProductTypeId = prod.IdCategorie;
            newArt.Name = prod.DenumireProdus;
            newArt.Description = prod.Mentiuni;
            newArt.Price = prod.Price;
            newArt.IsAvailable = true;
            db.Products.Add(newArt);
            db.SaveChanges();
            return JavaScript("Salvat");
        }

        //actiuni de stergere editare pentru produse si categorii
        public ActionResult StergeProdus(int idProdus)
        {

            Products art = db.Products.Where(x => x.Id == idProdus).First();
            db.Products.Remove(art);
            db.SaveChanges();
            return JavaScript("Sters");
        }



        public ActionResult StergeCategorieProdus(int idCategorie)
        {
            ProductTypes categ = new ProductTypes();
            categ = db.ProductTypes.Where(x => x.Id == idCategorie).First();
            if (categ.Products.Count() == 0)
            {
                foreach (var cat in db.ProductTypes)
                {
                    if (cat.Id_Parinte == idCategorie)
                    {
                        cat.Id_Parinte = null;
                    }
                }
                db.ProductTypes.Remove(categ);
                db.SaveChanges();
                return JavaScript("Sters");
            }
            return JavaScript("Eroare");
        }

        [HttpGet]
        public ActionResult EditareProdus(int idProdus)
        {
            Console.WriteLine(idProdus);
            GetEditProdus newProd = new GetEditProdus(db, idProdus);

            return PartialView("~/Views/Produse/_EditareProdus.cshtml", newProd);
        }



        [HttpPost]
        public ActionResult EditareProdus(PostEditProdus prod)
        {
            Products art = db.Products.Where(x => x.Id == prod.IdProdus).First();


            art.ProductTypeId = prod.IdCategorie;
            art.Name = prod.DenumireProdus;
            art.Description = prod.Mentiuni;
            art.Price = prod.Price;

            db.SaveChanges();
            return JavaScript(art.Id.ToString());
        }
        public ActionResult UploadImagine(PostUploadImagine uploadImg)
        {
            try
            {
                Products art = db.Products.Where(x => x.Id == uploadImg.Id).First();
                var binaryReader = new BinaryReader(uploadImg.ImageFile.InputStream);
                byte[] imagine = binaryReader.ReadBytes(uploadImg.ImageFile.ContentLength);
                Size dimensiuniNoi = new Size(200, 200);
                Image imagineDinBazaDeDate = Utilitati.ByteArrayToImage(imagine);
                Image imagineDinBazaDeDateRedimensionata = Utilitati.RedimensionareImagine(imagineDinBazaDeDate, dimensiuniNoi);
                byte[] imagineRedimensionataDeSalvatInBazaDeDate = Utilitati.ImageToByteArray(imagineDinBazaDeDateRedimensionata);
                art.ImagePath = imagineRedimensionataDeSalvatInBazaDeDate;
                db.SaveChanges();

                return JavaScript(art.Id.ToString());
            }
            catch (Exception ex)
            {
                return JavaScript("A apărut o eroare la încărcarea imaginii");
            }
        }
        public class Utilitati
        {
            public static Image RedimensionareImagine(Image imagine, Size dimensiuni)
            {
                return (new Bitmap(imagine, dimensiuni));
            }

            public static Image ByteArrayToImage(byte[] byteArrayIn)
            {

                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                return Image.FromStream(ms, true);
            }

            public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();

            }

        }

        public ActionResult StareProdus(int IdArt)
        {
            try
            {
                Products art = db.Products.Where(x => x.Id == IdArt).First();
               
                art.IsAvailable = !art.IsAvailable;
                db.SaveChanges();
                string str = art.IsAvailable ? "Activat" : "Inactivat";
                return JavaScript("Produsul a fost " + str);
            }
            catch (Exception ex)
            {

            }
            return JavaScript("eroare");
        }
    }
}