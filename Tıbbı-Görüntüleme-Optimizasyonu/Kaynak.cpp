#include <iostream>
#include <cmath>
#include <iomanip>

using namespace std;

// Parametreler (Sabitler)
const double LAMBDA = 2.0; // Hasta Geliş Hızı (lambda, hasta/saat)
const double CW = 200.0;   // Bekleme Maliyeti (Cw, TL/saat)
const double CI = 100.0;   // Boş Kalma Maliyeti (Ci, TL/saat)

// Kök Denklemi f(mu) = dK/dmu
// f(mu) = (CI * LAMBDA) / pow(mu, 2) - (CW * LAMBDA) / pow(mu - LAMBDA, 2)
double f(double mu) {
    // Kısıt Kontrolü: mu <= LAMBDA ise (sistem tıkanır) tanımsızlığı engelle
    if (mu <= LAMBDA) {
        return 1e9; // Büyük bir değer döndürerek bu bölgeyi engelle
    }
    return (CI * LAMBDA) / pow(mu, 2) - (CW * LAMBDA) / pow(mu - LAMBDA, 2);
}

// Türev Denklemi f'(mu)
// f'(mu) = -2 * (CI * LAMBDA) / pow(mu, 3) + 2 * (CW * LAMBDA) / pow(mu - LAMBDA, 3)
double f_prime(double mu) {
    if (mu <= LAMBDA) {
        return 1e9;
    }
    return (2 * CW * LAMBDA) / pow(mu - LAMBDA, 3) - (2 * CI * LAMBDA) / pow(mu, 3);
}

// Newton-Raphson Algoritması
void newton_raphson(double mu_initial, double tolerance, int max_iterations) {
    double mu_i = mu_initial;
    double mu_next;
    double error;
    int iteration = 0;

    cout << setprecision(6) << fixed;

    // Iterasyon Tablosu Başlığı
    cout << "\n--- 5. ADIM: SAYISAL ITERASYON TABLOSU ---\n";
    cout << "Iterasyon\tmu_i (Hizmet Hizi)\t\tf(mu_i)\t\t\tf'(mu_i)\t\tHata\n";
    cout << "----------------------------------------------------------------------------------------------------------------\n";

    do {
        // mu <= LAMBDA ise dur
        if (mu_i <= LAMBDA) {
            cout << "HATA: Kararsizlik kistina ulasildi (mu <= lambda)." << endl;
            break;
        }

        double f_val = f(mu_i);
        double fp_val = f_prime(mu_i);

        if (abs(fp_val) < 1e-9) {
            cout << "HATA: Payda sıfıra çok yakın (Türev Sıfır)." << endl;
            break;
        }

        // Newton-Raphson Formulu: mu_next = mu_i - f(mu_i) / f'(mu_i)
        mu_next = mu_i - f_val / fp_val;
        error = abs(mu_next - mu_i);

        cout << iteration << "\t\t" << mu_i << "\t\t" << f_val << "\t\t" << fp_val << "\t\t" << error << endl;

        mu_i = mu_next;
        iteration++;

    } while (error > tolerance && iteration < max_iterations);

    // Nihai Sonuçları Hesapla
    if (iteration <= max_iterations) {
        double mu_star = mu_i;
        double T_star_saat = 1.0 / mu_star;
        double T_star_dakika = T_star_saat * 60.0;
        double Ws_saat = 1.0 / (mu_star - LAMBDA); // Uzak randevu kısıtı kontrolü için Ws

        cout << "\n--- COZUM OZETI ---\n";
        cout << "Optimal Hizmet Hizi (mu*): " << mu_star << " hasta/saat\n";
        cout << "Optimal Randevu Suresi (T*): " << T_star_dakika << " dakika\n";
        cout << "Ortalama Sistem Suresi (Ws): " << Ws_saat << " saat\n";
        cout << "--------------------\n";
    }
}

int main() {

    cout << "--- TIBBI GORUNTULEME RANDEVU OPTIMIZASYONU ---" << endl;
    cout << "Gelis Hizi (lambda): " << LAMBDA << " hasta/saat" << endl;
    cout << "Cw (Bekleme Maliyeti): " << CW << " TL/saat, Ci (Bos Kalma Maliyeti): " << CI << " TL/saat" << endl;

    double mu_initial = 3.0;     // Baslangic Tahmini (mu > lambda = 2 olmali)
    double tolerance = 0.000001; // Hata toleransi (10^-6)
    int max_iterations = 50;     // Maksimum iterasyon sayisi

    newton_raphson(mu_initial, tolerance, max_iterations);

    return 0;
}