import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {

  async createKV(kv) {
    logger.log(kv)
    const res = await api.post('/api/vaultkeeps', kv)
    logger.log('createkv', res.data)
    AppState.vaultkeeps = res.data
  }

  async createVault(vaultData) {
    logger.log(vaultData)
    const res = await api.post('/api/vaults', vaultData)
    logger.log(res.data)
    AppState.myvaults = [...AppState.myvaults, res.data]
  }

  async getVaultById(id) {
    const res = await api.get("/api/vaults/" + id)
    logger.log('getvaultbyid', res.data)
    AppState.activeVault = res.data
  }

  async removeVault(id) {
    const res = await api.delete()
  }
}

export const vaultsService = new VaultsService()