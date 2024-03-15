import { useSearchParams } from "react-router-dom";
import * as Dialog from "@radix-ui/react-dialog";
import Close from "../assets/close.svg";

import "./Edit.css";
import { useCallback, useState } from "react";
import axios from "axios";

async function changeEmailStatus(id) {
  try {
    await axios.patch(`https://localhost:7286/api/Produtos/${id}`, {
      emailStatus: false,
    });
  } catch (error) {
    console.error("Erro ao alterar o status do produto:", error);
  }

}

function EmailModal({ toggleModalStatus, getProducts }) {
  const [email, setEmail] = useState();
  const [loading, setLoading] = useState(false);

  const [searchParams, setSearchParams] = useSearchParams();

  const id = searchParams.get("id");

  const sendEmail = useCallback(async () => {
    try {

      await axios.post(`https://localhost:7286/api/Email/enviar?destinatario=${email}&idProduto=${id}`);
  
      console.log("Email enviado com sucesso!");
  
      setLoading(false);
      toggleModalStatus();
  
      setSearchParams((params) => {
        params.delete("id");
        return params;
      });
  
      setEmail('');
      await changeEmailStatus(id);
      getProducts();

    } catch (error) {
      console.error("Erro ao enviar o email:", error);
    }
  }, [email, id, toggleModalStatus, setSearchParams]);

  const changeStatus = useCallback(async () => {
    await getProducts();

    setSearchParams((params) => {
      params.delete("id");
      return params;
    });
  }, [getProducts, setSearchParams]);

  const changeEmail = (e) => {
    setEmail(e.target.value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    setLoading(true);
    sendEmail();
  };

  return (
    <Dialog.Portal>
      <Dialog.Overlay className="DialogOverlay" />
      <Dialog.Content className="DialogContent">
        <Dialog.Close asChild>
          <button className="IconButton" onClick={changeStatus} aria-label="Close">
            <img className="close" src={Close} alt="" />
          </button>
        </Dialog.Close>
        <Dialog.Title className="DialogTitle">Enviar email</Dialog.Title>
        <Dialog.Description className="DialogDescription">
          Preencha o campo com o seu email e clique em "Enviar" para receber a
          melhor oferta!
        </Dialog.Description>
        <form onSubmit={handleSubmit}>
          <label className="Label" htmlFor="email">
            Email
          </label>
          <input
            type="email"
            required
            className="Input"
            id="email"
            onChange={changeEmail}
          />

          <div
            style={{
              display: "flex",
              marginTop: 25,
              justifyContent: "flex-end",
            }}
          >
            {!loading && (
              <button type="submit" className="Button green">
                Enviar
              </button>
            )}

            {loading && <button disabled className="loadingBtn">Enviando</button>}
          </div>
        </form>
      </Dialog.Content>
    </Dialog.Portal>
  );
}


export default EmailModal;
