import { useCallback, useEffect, useState } from "react";
import Header from "../../components/Header";
import * as Dialog from "@radix-ui/react-dialog";

import Xicon from "../../assets/xis.svg";
import Edit from "../../assets/edit.svg";
import Message from "../../assets/message.svg";
import DisabledMessage from "../../assets/disabledMessage.svg";
import lampadaAzul from "../../assets/bluelamp.svg";
import lampadaVermelha from "../../assets/redlamp.svg";

import Run from "../../assets/run.svg";
import Modal from "../../components/EditProductModal";

import "./Gestao.css";
import AddModal from "../../components/CreateProductModal";
import { price } from "../../utils/priceFormater";
import { useSearchParams } from "react-router-dom";
import EmailModal from "../../components/EmailModal";
import axios from "axios";
import BenchModal from "../../components/BenchModal";
import Delete from "../../components/DeleteModal";
import { Bounce, ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';


function Gestao() {
  const [list, setList] = useState();
  const [modalIsOpen, setModalOpen] = useState(false);
  const [infoModalIsOpen, setInfoModalOpen] = useState(false);
  const [emailModalIsOpen, setEmailModalOpen] = useState(false);
  const [deleteModalIsOpen, setDeleteModalOpen] = useState(false);
  const [searchParams, setSearchParams] = useSearchParams();

  async function changeEmailStatus(status,id) {
    try {
      await axios.patch(`https://localhost:8020/api/Produtos/${id}`, {
        emailStatus: status,
      });
    } catch (error) {
      console.error("Erro ao alterar o status do produto:", error);
    }
  }

  async function doBench(idproduto, nome) {
    try {
      const response = await toast.promise(
        axios.get(`https://localhost:8020/api/Benchmarking/compare?descricaoProduto=${nome}&idProd=${idproduto}`),
        {
          pending: 'Benchmarking em andamento...',
          success: 'Benchmarking concluído com sucesso!',
          error: 'Erro ao executar benchmarking 😞'
        }
      )
      if (response.data !== null && response.data.length > 0) {
        await Promise.all([
          await changeStatusPrice(true, response.data[3], idproduto),
          await changeEmailStatus(true,idproduto),
          
        ]);
      } else if(response.data=""){
        
      }
      
      else {
        await changeEmailStatus(false,idproduto),
        await changeStatusPrice(false, 0, idproduto);
        
      }
      
    } catch (error) {
      console.error("Erro ao executar o benchmarking:", error);
      await changeStatusPrice(false, 0, idproduto);
      await changeEmailStatus(false,idproduto);
      
    }
    getData();
  }

  async function changeStatusPrice(status, price, idProduto) {
    try {
      let response = await axios.patch(
        `https://localhost:8020/api/Produtos/${idProduto}`,
        {
          preco: price,
          status: status,
        }
      );
      console.log("CHANGE: " + response.data);
    } catch (error) {
      console.error("Erro ao alterar o status do produto:", error);
    }
  }

  async function getBench(idProduto) {
    try {
      let response = await axios.get(
        `https://localhost:8020/api/BenchmarkingItems/${idProduto}`
      );
      let mensagem = "uhu";
      if(response){
        let precoMag = response.data.precoLoja2;
        let precoMer = response.data.precoLoja1;
        let economia = response.data.economia;
        let linkMag = response.data.linkLoja2;
        let linkMer = response.data.linkLoja1;
  
        if (precoMag > precoMer) {
          mensagem = () => {return(
            <>
              <div className="message-box">
                <h4 className="message-title">O preço do produto está melhor no Mercado Livre, pois está <strong>R$ {economia}</strong> mais barato.</h4>
                <span className="message-link"> <strong>Economia:</strong> Link para produto no Mercado Livre: <a href={linkMer}>Clique Aqui</a></span>
                <span className="message-link"> Link para produto no Magazine Luiza: <a href={linkMag}>Clique Aqui</a></span>
              </div>
            </>
          )}
        } else if (precoMer > precoMag) {
          mensagem = () => {return(
            <>
              <div className="message-box">
                <h4 className="message-title">O preço do produto está melhor na Magazine Luiza, pois está <strong>R$ {economia}</strong> mais barato.</h4>
                <span className="message-link"> <strong>Economia:</strong> Link para produto no Magazine Luiza: <a href={linkMag}>Clique Aqui</a></span>
                <span className="message-link"> Link para produto no Mercado Livre: <a href={linkMer}>Clique Aqui</a></span>
              </div>
            </>
          )}
        } else {
          mensagem = () => {return(
            <>
              <div className="message-box">
                <h4>Valores Equivalentes</h4>
              </div>
            </>
          )}
        }
        return mensagem;
      }
      
    } catch (error) {
      console.log("Erro: "+ error)
    }
    
  }

  const toggleModal = () => {
    setModalOpen(!modalIsOpen);
  };

  const toggleEmailModal = () => {
    setEmailModalOpen(!emailModalIsOpen);
  };

  const toggleInfoModal = () => {
    setInfoModalOpen(!infoModalIsOpen);
  };

  const toggleDeleteModal = () => {
    setDeleteModalOpen(!deleteModalIsOpen);
  };

  const handleOnClickEdit = (product) => {
    setURLId(product.idProduto);
    toggleModal();
  }

  const setURLId = (id) => {
    setSearchParams((state) => {
      if (id) {
        state.set("id", id);
      }

      return state;
    });
  };

  

  const getData = useCallback(async () => {
    const response = await axios.get("https://localhost:8020/api/Produtos");
    setList(response.data);
  }, [list]);

  const createNewProduct = useCallback(
    async ({ name, price, estoque, estoqueMin }) => {
      await axios.post("https://localhost:8020/api/Produtos", {
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
          

          {

              modalIsOpen &&
          <Dialog.Root open={modalIsOpen} onOpenChange={setModalOpen}>
            <Modal
              toggleModalStatus={toggleModal}
              getProducts={getData}
            ></Modal>
          </Dialog.Root>
                   }

          <Dialog.Root open={emailModalIsOpen} onOpenChange={setEmailModalOpen}>
            <EmailModal
              toggleModalStatus={toggleEmailModal}
              getProducts={getData}
            ></EmailModal>
          </Dialog.Root>

          <Dialog.Root open={infoModalIsOpen} onOpenChange={setInfoModalOpen}>
            <BenchModal
              getInfo={getBench}
              toggleModalStatus={toggleInfoModal}
              getProducts={getData}
            ></BenchModal>
          </Dialog.Root>

          <Dialog.Root open={deleteModalIsOpen} onOpenChange={setDeleteModalOpen}>
            <Delete
              toggleModalStatus={toggleDeleteModal}
              getProducts={getData}
            ></Delete>
          </Dialog.Root>

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
                            <img
                              src={Run}
                              onClick={() => 
                                doBench(product.idProduto, product.descricao)
                              }
                              alt="Iniciar Benchmarking"
                            />
                          ) : product.status === true ? (
                            <img
                              src={lampadaAzul}
                              onClick={() => {
                                toggleInfoModal();
                                setURLId(product.idProduto);

                              }}
                              alt="Iniciar Benchmarking"
                            />
                          ) : (
                            <img
                              src={lampadaVermelha}
                              onClick={() =>
                                doBench(product.idProduto, product.descricao)
                              }
                              alt="Iniciar Benchmarking"
                            />
                          )}
                        </button>

                        

                        <button
                          onClick={() => {
                            setURLId(product.idProduto);
                            toggleEmailModal();
                            
                          }}
                          disabled={
                            product.status !== true 
                            
                            &&

                            product.emailStatus !== true
                          }
                          className="sendButton"
                        >
                          {product.status === true 
                          &&
                          product.emailStatus === true ? (
                            <img src={Message} alt="Enviar Email" />
                          ) : (
                            <img src={DisabledMessage} alt="Enviar Email" />
                          )}
                        </button>

                        <button
                          onClick={() => {
                            setURLId(product.idProduto);
                            toggleModal();}
                          }>
                          <img src={Edit} alt="Editar" />
                        </button>

                        <button
                          onClick={() => 
                            {
                              setURLId(product.idProduto);
                              toggleDeleteModal()
                            }}>
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
      <ToastContainer
        position="top-right"
        autoClose={4000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="dark"
        transition={Bounce}
      />

    </>
  );
}

export default Gestao;
