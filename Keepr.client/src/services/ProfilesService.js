import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {
  async getProfileById(id) {
    const res = await api.get('/api/profiles/' + id)
    logger.log('getprofilebyid', res.data)
    AppState.activeProfile = res.data
  }
  async getVaultsByProfileId(id) {
    const res = await api.get('/api/profiles/' + id + '/vaults')
    logger.log('getvaultsbyprofileid', res.data)
    AppState.profilevaults = res.data
  }
  async getKeepsByProfileId(id) {
    const res = await api.get('/api/profiles/' + id + '/keeps')
    logger.log('getkeepsbyprofileid', res.data)
    AppState.profilekeeps = res.data
  }
}

export const profilesService = new ProfilesService()