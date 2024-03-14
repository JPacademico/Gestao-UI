import { useCallback, useEffect, useState } from "react";
import Header from "../components/Header";
import * as Dialog from "@radix-ui/react-dialog";

import Xicon from "../assets/xis.svg";
import Edit from "../assets/edit.svg";
import Message from "../assets/message.svg";
import DisabledMessage from "../assets/disabledMessage.svg";
import lampadaAzul from "../assets/bluelamp.svg"
import lampadaVermelha from "../assets/redlamp.svg"

import Run from "../assets/run.svg";
import Modal from "../components/EditProductModal";

import "./Gestao.css";
import AddModal from "../components/CreateProductModal";
import api from "../lib/api";
import { price } from "../utils/priceFormater";
import { useSearchParams } from "react-router-dom";
import EmailModal from "../components/EmailModal";
import axios from "axios";



function Gestao() {
  const [list, setList] = useState();
  const [modalIsOpen, setModalOpen] = useState(false);
  const [emailModalIsOpen, setEmailModalOpen] = useState(false);
  const [searchParams, setSearchParams] = useSearchParams();
  const [benchStatus, setBenchStatus] = useState(null)


  async function doBench(idproduto,nome) {
    let response = await axios.get(`https://localhost:7286/api/Benchmarking/compare?descricaoProduto=${nome}&idProd=${idproduto}`);
    console.log(response.data)
    if (response.data !== null) {
      changePrice(response.data[3], idproduto)
      changeStatus(true, idproduto)
    } else {
      changeStatus(false, idproduto)
    }
  }

  async function changePrice(price, idProduto){
    let response = await axios.patch(`https://localhost:7286/api/Produtos/${idProduto}`, {
    preco: price,
  });
    console.log("CHANGEPRICE: "+response.data)
    getData();
  }

  async function changeStatus(status, idProduto){
    let response = await axios.patch(`https://localhost:7286/api/Produtos/${idProduto}`, {
    status: status,
    
  });
    getData();
    console.log("CHANGESTATUS: "+response.data)
  }

  const toggleModal = () => {
    setModalOpen(modalIsOpen === true ? false : true);
  };

  const toggleEmailModal = () => {
    setEmailModalOpen(emailModalIsOpen === true ? false : true);
  };

  const setURLId = (id) => {
    setSearchParams((state) => {
      if (id) {
        state.set("id", id);
      }

      return state;
    });
  };

  const handleDeleteProduct = useCallback(async (id) => {
    await axios.delete(`https://localhost:7286/api/Produtos/${id}`);
    setList((state) => state.filter((product) => product.id != id));
    getData();
  }, []);

  const getData = useCallback(async () => {
    const response = await axios.get("https://localhost:7286/api/Produtos");
    setList(response.data);
    console.log(list);
  }, [list]);

  const createNewProduct = useCallback(
    async ({ name, price, estoque, estoqueMin }) => {
      await axios.post("https://localhost:7286/api/Produtos", {
        descricao: name,
        preco: price,
        estoqueAtual: estoque,
        estoqueMinimo: estoqueMin,
      });

      getData();
    }
  );

  useEffect(() => {
    getData();
  }, []);

  return (
    <>
      <Header />
      <div className="intro-box">
        <h4>Gestão de Produtos</h4>
        <Dialog.Root>
          <Dialog.Trigger asChild>
            <button className="btn-new">NOVO</button>
          </Dialog.Trigger>
          <AddModal createNewModal={createNewProduct}></AddModal>
        </Dialog.Root>
      </div>

      <div className="table-box">
        <table>
          <thead>
            <tr>
              <th>Código</th>
              <th>Descrição</th>
              <th>Preço</th>
              <th>Estoque Atual</th>
              <th>Estoque Mínimo</th>
              <th>Opções</th>
            </tr>
          </thead>

          <Dialog.Root open={modalIsOpen} onOpenChange={setModalOpen}>
            <Modal
              toggleModalStatus={toggleModal}
              getProducts={getData}
            ></Modal>
          </Dialog.Root>

          <Dialog.Root open={emailModalIsOpen} onOpenChange={setEmailModalOpen}>
            <EmailModal
              toggleModalStatus={toggleEmailModal}
              getProducts={getData}
            ></EmailModal>
          </Dialog.Root>

          {/* <Dialog.Root open={emailModalIsOpen} onOpenChange={setEmailModalOpen}>
            <EmailModal
              toggleModalStatus={toggleEmailModal}
              getProducts={getData}
            ></EmailModal>
          </Dialog.Root> */}
          
          <tbody>
            {list &&
              list.map((product) => {
                return (
                  <tr key={product.idProduto}>
                    <td>{product.idProduto}</td>
                    <td>{product.descricao}</td>
                    <td>{price.format(product.preco)}</td>
                    <td>{product.estoqueAtual}</td>
                    <td>{product.estoqueMinimo}</td>
                    <td>
                      <div className="icons-box">
                        <button>
                          {product.status == null ? (
                            <img src={Run} onClick={() => doBench(product.idProduto, product.descricao)} alt="Iniciar Benchmarking" />
                          ) : product.status == true ? (
                            <img src={lampadaAzul} onClick={() => doBench(product.idProduto, product.descricao)} alt="Iniciar Benchmarking" />
                          ) : product.status == false &&(
                            <img src={lampadaVermelha} onClick={() => doBench(product.idProduto, product.descricao)} alt="Iniciar Benchmarking" />
                          )
                          }
                          

                        </button>

                        <button
                          onClick={() => {
                            toggleEmailModal();
                            setURLId(product.idProduto);
                          }}
                          disabled={product.status !== true}
                          className="sendButton"

                        >
                          {product.status === true ? (
                            <img src={Message} alt="Enviar Email" />
                          ) : (
                            <img src={DisabledMessage} alt="Enviar Email" />
                          )}
                        </button>

                        <button
                          onClick={() => {
                            setURLId(product.idProduto);
                            toggleModal();
                          }}
                        >
                          <img src={Edit} alt="Editar" />
                        </button>

                        <button onClick={() => handleDeleteProduct(product.idProduto)}>
                          <img src={Xicon} alt="Apagar" />
                        </button>
                      </div>
                    </td>
                  </tr>
                );
              })}
          </tbody>
        </table>
      </div>
    </>
  );
}

export default Gestao;
