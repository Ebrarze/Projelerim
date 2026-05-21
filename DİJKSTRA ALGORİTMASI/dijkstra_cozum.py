# -*- coding: utf-8 -*-
"""
Ders: Ayrık Matematik - Dijkstra Algoritması Ödevi
Ad Soyad: Zehra Ebrar Erbağ
Numara: B241200043
Bölüm: Bilişim Sistemleri Mühendisliği
Başlangıç Düğümü: 0
"""

import numpy as np

# --- 1. ADIM: GRAFIN AĞIRLIK (KOMŞULUK) MATRİSİNİN OLUŞTURULMASI ---
# Grafta toplam 16 düğüm (0'dan 15'e kadar) olduğu için 16x16 boyutunda bir matris oluşturuyoruz.
# İki düğüm arasında doğrudan bağlantı yoksa kitap mantığına uygun olarak '0' değeri verilir.
W = np.zeros((16, 16))

# Görseldeki mil mesafelerine göre kenar ağırlıklarını matrise simetrik (çift yönlü) olarak işliyoruz.
edges = [
    (0, 1, 0.3), (1, 2, 0.1), (1, 8, 0.4), (1, 12, 4.5),
    (2, 3, 1.5), (3, 4, 0.3), (3, 12, 7.0), (4, 5, 0.2),
    (5, 6, 0.2), (6, 7, 2.7), (7, 15, 5.9), (8, 9, 2.1),
    (9, 10, 1.4), (10, 11, 1.6), (11, 12, 0.6), (12, 13, 0.6),
    (13, 14, 0.4), (14, 15, 0.2)
]

for u, v, w in edges:
    W[u, v] = w
    W[v, u] = w  # Graf yönlü olmadığı için iki yönü de eşitliyoruz.


# --- 2. ADIM: DIJKSTRA ALGORİTMASI FONKSİYONU ---
def dijkstra(W, start):
    # Düğüm sayısı matrisin satır boyutundan bulunur.
    n = W.shape[0]
    
    # Başlangıçta tüm düğümlere olan uzaklıkları sonsuz (inf) kabul ediyoruz.
    distances = np.array([np.inf] * n)
    
    # Her düğüme hangi düğümden gelindiğini (yol takibi için) tutan dizi.
    previous = np.array([np.inf] * n)
    
    # Düğümlerin ziyaret edilip edilmediğini kontrol eden Boolean dizi.
    visited = np.array([False] * n)
    
    # Başlangıç düğümünün kendisine olan uzaklığı her zaman 0'dır.
    distances[start] = 0
    
    # Tüm düğümleri tek tek işlemek için döngü başlatıyoruz.
    for _ in range(n):
        # Henüz ziyaret edilmemiş düğümler arasından uzaklığı en küçük olan düğümü seçiyoruz.
        min_distance = np.inf
        current = None
        
        for i in range(n):
            if not visited[i] and distances[i] < min_distance:
                min_distance = distances[i]
                current = i
                
        # Eğer seçilecek veya ulaşılabilecek düğüm kalmadıysa algoritmayı sonlandırıyoruz.
        if current is None:
            break
            
        # Seçilen düğümü ziyaret edildi (True) olarak işaretliyoruz.
        visited[current] = True
        
        # Seçilen düğümün komşularını kontrol edip mesafeleri güncelliyoruz.
        for neighbor in range(n):
            # Eğer iki düğüm arasında bağlantı varsa (W > 0) ve komşu ziyaret edilmediyse:
            if W[current, neighbor] > 0 and not visited[neighbor]:
                # Başlangıç noktasından komşu düğüme mevcut düğüm üzerinden yeni mesafeyi hesaplıyoruz.
                new_distance = distances[current] + W[current, neighbor]
                
                # Eğer yeni bulunan yol, eski bilinen yoldan daha kısaysa güncelleme yapıyoruz.
                if new_distance < distances[neighbor]:
                    distances[neighbor] = new_distance
                    previous[neighbor] = current
                    
    return distances, previous


# --- 3. ADIM: EN KISA YOLU YAZDIRAN FONKSİYON ---
def print_path(previous, start, destination):
    path = []
    current = destination
    
    # Hedef düğümden başlayarak geriye doğru başlangıç düğümüne kadar gidiyoruz.
    while current != start:
        if current == np.inf or current is None:
            print("Yol bulunamadı")
            return
        path.append(int(current))
        current = previous[int(current)]
        
    # Başlangıç düğümünü de yola ekliyoruz.
    path.append(start)
    
    # Yol geriye doğru oluşturulduğu için başlangıçtan hedefe okumak adına ters çeviriyoruz.
    path.reverse()
    
    # Rotaları okunaklı bir şekilde ok işaretleriyle birleştirerek yazdırıyoruz.
    print("Yol:", " -> ".join(map(str, path)))


# --- 4. ADIM: ALGORİTMAYI ÇALIŞTIRMA VE ÇIKTILARI ALMA ---
start_node = 0
distances, previous = dijkstra(W, start_node)

print("\n==================================================================")
print("                    EN KISA MESAFELER VE YOLLAR                   ")
print("==================================================================")

for i in range(len(W)):
    print(f"\n[0 düğümünden {i} düğümüne:]")
    if distances[i] == np.inf:
        print("Yol bulunamadı.")
    else:
        print_path(previous, start_node, i)
        print(f"Toplam uzaklık = {round(distances[i], 2)} mil")

print("==================================================================")