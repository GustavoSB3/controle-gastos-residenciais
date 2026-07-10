import { useState } from 'react'
import { PessoasPage } from './pages/PessoasPage'
import { TransacoesPage } from './pages/TransacoesPage'
import { TotaisPage } from './pages/TotaisPage'
import './App.css'

type Pagina = 'pessoas' | 'transacoes' | 'totais'

export default function App() {
  const [pagina, setPagina] = useState<Pagina>('pessoas')
  return <><header><div><h1>Controle de Gastos</h1><p>Organize as finanças da sua residência</p></div>
    <nav>{(['pessoas', 'transacoes', 'totais'] as Pagina[]).map(item => <button key={item} className={pagina === item ? 'ativo' : ''} onClick={() => setPagina(item)}>{item[0].toUpperCase() + item.slice(1)}</button>)}</nav>
  </header><main>{pagina === 'pessoas' && <PessoasPage />}{pagina === 'transacoes' && <TransacoesPage />}{pagina === 'totais' && <TotaisPage />}</main></>
}
